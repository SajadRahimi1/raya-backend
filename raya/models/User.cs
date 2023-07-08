using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class User : BaseEntity
{
    public Guid Token { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? FatherName { get; set; }

    public string? Birthday { get; set; }

    public string? BornCity { get; set; }

    public string? NationalCode { get; set; }

    public string? NationalNumber { get; set; }

    public string? Education { get; set; }

    public string? Address { get; set; }

    public string? EmergancyNumber { get; set; }

    public string? code { get; set; } = null;

    public List<ClassCategory> ReservedClasses { get; set; } = new List<ClassCategory>();
    public List<ReserveNurse> ReserveNurses { get; set; } = new List<ReserveNurse>();
}