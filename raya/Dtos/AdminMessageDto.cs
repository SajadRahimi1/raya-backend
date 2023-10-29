using System.ComponentModel.DataAnnotations;

public class AdminMessageDto
{
    [Required] public String? Content { get; set; }

    public string MessageType { get; set; } = "text";

    public IFormFile? File { get; set; }

    public string? userId { get; set; }

}