using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using hotchocolate_fullstack_demo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // added CORS before anything else to allow all origins
        services.AddCors((corsOptions) =>
        {
            corsOptions.AddDefaultPolicy(new CorsPolicy()
            {
                IsOriginAllowed = (origin) => true,
            });
        });

        // dependency injection for the pokemon DB context
        services.AddDbContext<PokemonContext>();

        // dependency injection for the graph QL schema
        services.AddGraphQL(
            SchemaBuilder
                .New()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .Create(),
            new QueryExecutionOptions() { ForceSerialExecution = true });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // added CORS before everything so any origin can do stuff
        app.UseCors((corsPolicyBuilder) =>
        {
            corsPolicyBuilder.AllowAnyOrigin();
            corsPolicyBuilder.AllowAnyHeader();
            corsPolicyBuilder.AllowAnyMethod();
        });

        // initialize the database
        InitializeDatabase(app);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        // added graph QL usage to the application
        app.UseGraphQL();

        // added playground usage to the application
        app.UsePlayground();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<PokemonContext>();
            if (context.Database.EnsureCreated())
            {
                var bulbasaur = new Pokemon() { id = 1, name = "Bulbasaur", type = "Grass" };
                var ivysaur = new Pokemon() { id = 2, name = "Ivysaur", type = "Grass" };
                var venasaur = new Pokemon() { id = 3, name = "Venasaur", type = "Grass" };
                var charmander = new Pokemon() { id = 4, name = "Charmander", type = "Fire" };
                var charmeleon = new Pokemon() { id = 5, name = "Charmeleon", type = "Fire" };
                var charizard = new Pokemon() { id = 6, name = "Charizard", type = "Fire" };
                var squirtle = new Pokemon() { id = 7, name = "Squirtle", type = "Water" };
                var wartortle = new Pokemon() { id = 8, name = "Wartortle", type = "Water" };
                var blastoise = new Pokemon() { id = 9, name = "Blastoise", type = "Water" };

                context.Pokemon.AddRange(
                    bulbasaur,
                    ivysaur,
                    venasaur,
                    charmander,
                    charmeleon,
                    charizard,
                    squirtle,
                    wartortle,
                    blastoise);

                context.SaveChangesAsync();
            }
        }
    }
}