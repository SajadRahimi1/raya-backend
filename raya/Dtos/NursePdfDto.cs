using System.ComponentModel.DataAnnotations;

public class NursePdfDto
{
    [Required]
    public string nurseId { get; set; }

    [Required,ValidatePdf]
    public IFormFile pdf { get; set; }
}