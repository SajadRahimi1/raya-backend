using System.ComponentModel.DataAnnotations;

public class NurseStatusDto
{
    [Required]
    public string nurseId { get; set; }

    [Required]
    public Status status { get; set; }

}