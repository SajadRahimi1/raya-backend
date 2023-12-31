using System.ComponentModel.DataAnnotations;

public class SendMessageDto
{
    public String? Content { get; set; }

    public string MessageType { get; set; } = "text";
    
    public IFormFile? File { get; set; }

}