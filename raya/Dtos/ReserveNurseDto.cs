using System.ComponentModel.DataAnnotations;

public class ReserveNurseDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid NurseId { get; set; }

    [Required]
    public List<Days> days { get; set; }


}