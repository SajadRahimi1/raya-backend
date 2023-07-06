using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;


[PrimaryKey(nameof(Id))]
public class ReserveNurse : BaseEntity
{
    
    public Nurse Nurse { get; set; }

    public Guid NurseId { get; set; }

    public User UserReserved { get; set; }

    public Guid UserId { get; set; }

    public string? Days { get; set; }

}

