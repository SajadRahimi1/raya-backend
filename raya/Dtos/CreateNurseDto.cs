using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreateNurseDto
{

    [Required]
    public string Name { get; set; }

    [Required]
    public string Locations { get; set; }

    [Required]
    public string About { get; set; }

    [Required]
    public string days { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NurseCategory NurseCategory { get; set; }

}