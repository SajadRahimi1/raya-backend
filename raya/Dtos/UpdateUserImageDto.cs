using System.ComponentModel.DataAnnotations;

public class UpdateUserImageDto
{
    [Required]
    public IFormFile image { get; set; }
}