using Microsoft.EntityFrameworkCore;


[PrimaryKey(nameof(Id))]
public class ReserveNurse : BaseEntity
{

    public User UserReserved { get; set; }

    public Guid UserId { get; set; }

    public bool isDeleted { get; set; } = false;

    public NurseCategory NurseCategory { get; set; }

    public string? description { get; set; }

    public Gender Gender { get; set; }

    public string? Age { get; set; }

    public List<Problem> Problems { get; set; } = new List<Problem>();

    public List<int> Ages { get; set; } = new List<int>();

    public Shift Shift { get; set; }

    public string? Hours { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }

    public string? PeopleInHouse { get; set; }

    public bool CCTV { get; set; }

    public string? problem { get; set; }

    public string? Address { get; set; }

    public string? name { get; set; }

    public string? phoneNumber { get; set; }
    public string? howToKnow { get; set; }

    public string? Province { get; set; }

    public string? City { get; set; }

    public string? Neighborhood { get; set; }

}


public enum Gender
{
    Male,
    Female,
    Both
}

public enum Shift
{
    Boarding,
    Day,
    Night,
    Hour
}

public enum Problem
{
    Bedpan,
    Diaper,
    Parkinson,
    Son,
    Alzheimer,
    MS,
    None
}
