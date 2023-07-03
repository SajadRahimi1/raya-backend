using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class ReserveNurse:BaseEntity{
    [ForeignKey(nameof(Nurse.Id))]
    public Guid NurseId { get; set; }

    [NotMapped]
    public List<string> days { get; set; }=new List<string>();
}