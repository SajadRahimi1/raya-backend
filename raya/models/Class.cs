using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Class : BaseEntity
{
    public string? Title { get; set; }

    public List<ClassCategory> ClassCategories { get; set; }
    
}