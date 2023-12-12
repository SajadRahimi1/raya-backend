using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Nurse : BaseEntity
{
    public Guid? userId { get; set; }
    public string Name { get; set; } = "";
    public string FatherName { get; set; } = "";
    public string Birthday { get; set; } = "";
    public string BornCity { get; set; } = "";
    public string NationalCode { get; set; } = "";
    public string NationalNumber { get; set; } = "";
    public string Education { get; set; } = "";
    public int formCode { get; set; } = 0;
    public string Address { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string HomeNumber { get; set; } = "";
    public string Shift { get; set; } = "";
    public string? Province { get; set; }
    public string? City { get; set; }
    public string? Neighborhood { get; set; }
    public string? Street { get; set; }
    public string? Alley { get; set; }
    public string? PostalCode { get; set; }
    public string? Unit { get; set; }
    public bool SpecialCare { get; set; }
    public string? HusbandPhoneNumber { get; set; }
    public string? ChildPhoneNumber { get; set; }
    public string? ParentPhoneNumber { get; set; }
    public string? pdfLink { get; set; }
    public string? authority { get; set; }

    public string? OtherProp { get; set; }

    public List<OtherProp> OtherProps { get; set; } = new List<OtherProp>();

    public Guarantee? Guarantee { get; set; }

    public Status? status { get; set; }

    public NurseImages? NurseImages { get; set; }

    public List<NurseFamily> NurseFamily { get; set; } = new List<NurseFamily>();

    public string NurseCategory { get; set; }

    public List<ReserveNurse> ReserveNurses { get; set; } = new List<ReserveNurse>();

    public List<NurseCategory> NurseCategories { get; set; } = new List<NurseCategory>();

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

public enum Status
{
    Confirmed,
    Deny,
    Deleted
}

public enum OtherProp
{
    Drug,
    Smoke,
    Alcoholic,
    Disability,
    Criminal,
    Family,
    SpecializedDegree
}