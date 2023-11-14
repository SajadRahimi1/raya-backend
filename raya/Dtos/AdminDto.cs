using System.ComponentModel.DataAnnotations;

public class AdminDto
{
    public string? name { get; set; }
    public string? phoneNumber { get; set; }
    [Required] public string? username { get; set; }
    [Required] public string? password { get; set; }
    public bool isEnable { get; set; }
}