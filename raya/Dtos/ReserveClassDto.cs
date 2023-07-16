using System.ComponentModel.DataAnnotations;

public class ReserveClassDto
{


    [Required]
    public Guid ClassCategoryId { get; set; }

    public Guid? UserId { get; set; }

    [Required]
    public string Day { get; set; }

    [Required]
    public string Hours { get; set; }

    public bool IsInstallment { get; set; } = false;

}