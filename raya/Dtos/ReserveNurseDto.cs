using System.ComponentModel.DataAnnotations;

public class ReserveNurseDto
{    
    public Guid? UserId { get; set; }

    [Required]
    public Guid NurseId { get; set; }

    [Required] 
    public string Days { get; set; }
}