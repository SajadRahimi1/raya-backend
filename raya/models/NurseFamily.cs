using System.ComponentModel.DataAnnotations.Schema;

public class NurseFamily : BaseEntity
{
    [NotMapped]
    public Nurse Nurse { get; set; }
    public Guid NurseId { get; set; }
    public string Name { get; set; } = "";
    public string Information { get; set; } = "";
    public string KnowTime { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
}