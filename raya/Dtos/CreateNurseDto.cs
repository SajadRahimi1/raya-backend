using System.ComponentModel.DataAnnotations;

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

}