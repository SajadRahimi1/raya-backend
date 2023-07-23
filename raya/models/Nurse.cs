using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Nurse : BaseEntity
{
    public string Name { get; set; } = "";
    public string FatherName { get; set; } = "";
    public string Birthday { get; set; } = "";
    public string BornCity { get; set; } = "";
    public string NationalCode { get; set; } = "";
    public string NationalNumber { get; set; } = "";
    public string Education { get; set; } = "";
    public string Address { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string HomeNumber { get; set; } = "";
    public bool SpecialCare { get; set; }

    public string? HusbandPhoneNumber { get; set; }
    public string? ChildPhoneNumber { get; set; }
    public string? ParentPhoneNumber { get; set; }

    public Guarantee Guarantee { get; set; }
    
    public NurseImages NurseImages { get; set; }
    public List<NurseFamily> NurseFamily { get; set; }

    public NurseCategory NurseCategory { get; set; }

    public List<ReserveNurse> ReserveNurses { get; set; } = new List<ReserveNurse>();

}

public enum NurseCategory
{
    Kid,
    Oldage,
    Patient,
    All
}

public enum Guarantee
{
    Promissory,
    Check,
    BusinessLicense,
    Representative
}