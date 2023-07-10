using System.ComponentModel.DataAnnotations;


public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}