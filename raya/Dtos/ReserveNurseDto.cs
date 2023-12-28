using System.ComponentModel.DataAnnotations;

public class ReserveNurseDto
{
    public Guid? UserId { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public NurseCategory NurseCategory { get; set; }

    public string? Age { get; set; }

    [Required]
    public Shift Shift { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    public string? Hours { get; set; }

    public string? howToKnow { get; set; }


    public string? From { get; set; }
    public string? To { get; set; }

    public string? PeopleInHouse { get; set; }

    public List<Problem> Problems { get; set; } = new List<Problem>();

    public List<int?> Ages { get; set; } = new List<int?>();

    [Required]
    public bool CCTV { get; set; }

    [Required]
    public string? Address { get; set; }

    public string? problem { get; set; }

    public string? Description { get; set; }

    public string? Province { get; set; }
    public string? City { get; set; }
    public string? Neighborhood { get; set; }

}