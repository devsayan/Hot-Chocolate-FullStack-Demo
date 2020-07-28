using System.ComponentModel.DataAnnotations.Schema;

[Table("Pokemon")]
public partial class Pokemon
{
    public int id { get; set; }

    public string name { get; set; }

    public string type { get; set; }
}