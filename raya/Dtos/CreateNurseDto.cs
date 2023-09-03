using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    [Required]
    public string PhoneNumber { get; set; } = "";

    [Required]
    public string HomeNumber { get; set; } = "";

    [Required]
    public bool SpecialCare { get; set; }

    public string? OtherProp { get; set; }

    [Required]
    public string NurseCategory { get; set; }

}