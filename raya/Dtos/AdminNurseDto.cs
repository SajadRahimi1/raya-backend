using System.ComponentModel.DataAnnotations;

public class AdminNurseDto
{
    
    
    [Required]
    public Guid NurseId { get; set; }
    
    
    public IFormFile? Picture { get; set; }
    
     
    public IFormFile? FirstPageImage { get; set; }
    
    
    public IFormFile? DescriptionImage { get; set; }
        
    public IFormFile? AgreementImage { get; set; }
    
}