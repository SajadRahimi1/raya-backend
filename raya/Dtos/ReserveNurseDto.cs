using System.ComponentModel.DataAnnotations;

public class ReserveNurseDto
{
    public Guid? UserId { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public NurseCategory NurseCategory { get; set; }

    [Required]
    public string Age { get; set; }

    [Required]
    public Shift Shift { get; set; }

    [Required]
    public string Hours { get; set; }

    [Required]
    public string PeopleInHouse { get; set; }

    [Required]
    public bool CCTV { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string description { get; set; }

}