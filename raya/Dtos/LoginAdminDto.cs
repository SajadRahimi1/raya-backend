using System.ComponentModel.DataAnnotations;

public class LoginAdminDto
{
    [Required]
    public string username { get; set; }

    [Required]
    public string password { get; set; }
}