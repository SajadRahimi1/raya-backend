using System.ComponentModel.DataAnnotations;

public class NurseDto
{
    [Required] public string Name { get; set; } = "";
    [Required] public string FatherName { get; set; } = "";
    [Required] public string Birthday { get; set; } = "";
    [Required] public string BornCity { get; set; } = "";
    [Required] public string NationalCode { get; set; } = "";
    [Required] public string NationalNumber { get; set; } = "";
    [Required] public string Education { get; set; } = "";
    [Required] public int formCode { get; set; } = 0;
    [Required] public string Address { get; set; } = "";
    [Required] public string PhoneNumber { get; set; } = "";
    public string HomeNumber { get; set; } = "";
    [Required] public string Shift { get; set; } = "";
    [Required] public bool SpecialCare { get; set; }

    public string? HusbandPhoneNumber { get; set; }
    public string? ChildPhoneNumber { get; set; }
  public string? ParentPhoneNumber { get; set; }
     public string? pdfLink { get; set; }
     public string? authority { get; set; }

    public string? OtherProp { get; set; }

    [Required] public Guarantee? Guarantee { get; set; }

    [Required] public Status? status { get; set; }


    [Required] public string NurseCategory { get; set; }


}
