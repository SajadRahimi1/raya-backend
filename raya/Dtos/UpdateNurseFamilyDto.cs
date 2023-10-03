using System.ComponentModel.DataAnnotations;

public class UpdateNurseFamilyDto
{
    [Required]
    public Guid NurseId { get; set; }
    public string? husbandPhoneNumber { get; set; }
    public string? childPhoneNumber { get; set; }
    public string? parentPhoneNumber { get; set; }
    public string? guarantee { get; set; }

    [Required]
    public List<NurseFamilyDto> nurseFamily { get; set; }
}

public class NurseFamilyDto
{

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Information { get; set; } = "";

    [Required]
    public string KnowTime { get; set; } = "";

    [Required]
    public string PhoneNumber { get; set; } = "";
}