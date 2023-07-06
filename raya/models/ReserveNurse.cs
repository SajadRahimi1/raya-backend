using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[PrimaryKey(nameof(Id))]
public class ReserveNurse : BaseEntity
{

    public Nurse Nurse { get; set; }

    public Guid NurseId { get; set; }

    public User UserReserved { get; set; }

    public Guid UserId { get; set; }

    public List<string> Days { get; set; } = new List<string>();

}

