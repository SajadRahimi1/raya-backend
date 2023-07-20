using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Nurse : BaseEntity
{
    public string Name { get; set; }
    public string? Locations { get; set; }
    public string About { get; set; } = "";

    public string? Days { get; set; }
    public string? Image { get; set; }
    public double Rating { get; set; } = 0;
    public NurseCategory NurseCategory { get; set; }

    public List<ReserveNurse> ReserveNurses { get; set; } = new List<ReserveNurse>();

}

public enum NurseCategory{
    Kid,
    Oldage,
    Patient
}