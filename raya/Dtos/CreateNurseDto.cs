using System.ComponentModel.DataAnnotations;

public class CreateNurseDto
{
    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string FatherName { get; set; } = "";

    [Required]
    public string Birthday { get; set; } = "";

    [Required]
    public string BornCity { get; set; } = "";

    [Required]
    public string NationalCode { get; set; } = "";

    [Required]
    public string NationalNumber { get; set; } = "";

    [Required]
    public string Education { get; set; } = "";

    [Required]
    public string Address { get; set; } = "";

   
    public string? Shift { get; set; }

    [Required]
    public string PhoneNumber { get; set; } = "";
    
    public string HomeNumber { get; set; } = "";

    [Required]
    public bool SpecialCare { get; set; }

    public string? OtherProp { get; set; }

    
    public string? NurseCategory { get; set; }

    public string? Province { get; set; }
    public string? City { get; set; }
    public string? Neighborhood { get; set; }
    public string? Street { get; set; }
    public string? Alley { get; set; }
    public string? PostalCode{ get; set; }
    public List<OtherProp> OtherProps { get; set; }=new List<OtherProp>();

    public List<Shift> Shifts { get; set; } = new List<Shift>();

    public IEnumerable<NurseCategory> NurseCategories { get; set; } = new List<NurseCategory>();
    public string? Unit { get; set; }

}