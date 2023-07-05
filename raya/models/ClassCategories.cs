using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class ClassCategory : BaseEntity
{
    public Class Class { get; set; }

    public Guid ClassId { get; set; }
    
    public string? Title { get; set; }

    [NotMapped]
    public List<string> Hours { get; set; } = new List<string>();

    [NotMapped]
    public List<string> Days { get; set; } = new List<string>();

    public string? TotallHours { get; set; }

    public string? TimeHolding { get; set; }

    public string? Price { get; set; }

    public string? PrePaid { get; set; }

    public int InstallmentNumber { get; set; }

    public string? InstallmentPrice { get; set; }

    public List<User> UsersReserved { get; set; }= new List<User>();
}