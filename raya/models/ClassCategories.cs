using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class ClassCategory : BaseEntity
{
    public Class Class { get; set; }

    public Guid ClassId { get; set; }
    
    public string? Title { get; set; }

    public string? Hours { get; set; }

    public string Days { get; set; } = "روزهای زوج,روزهای فرد";

    public string? TotallHours { get; set; }

    public string? TimeHolding { get; set; }

    public string? Price { get; set; }

    public string? PrePaid { get; set; }

    public int InstallmentNumber { get; set; }

    public string? InstallmentPrice { get; set; }

    public List<ReserveClass> ReserveClasses { get; set; }= new List<ReserveClass>();
}