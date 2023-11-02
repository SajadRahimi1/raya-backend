using System.ComponentModel.DataAnnotations;

public class NurseUploadsDto
{
    
    
    [Required]
    public Guid NurseId { get; set; }
    
    [Required]
    public IFormFile? Picture { get; set; }
    
    [Required] 
    public IFormFile? FirstPageImage { get; set; }
    
    [Required]
    public IFormFile? DescriptionImage { get; set; }
        
    public IFormFile? AgreementImage { get; set; }
    
}