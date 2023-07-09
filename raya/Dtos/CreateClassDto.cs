using System.ComponentModel.DataAnnotations;

public class CreateClassDto
{
    [Required]
    public string title { get; set; }

    [Required]
    public IFormFile image { get; set; }

}