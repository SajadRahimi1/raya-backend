public class ReserveClass:BaseEntity
{
    public ClassCategory ClassCategory { get; set; }

    public Guid ClassCategoryId { get; set; }

    public User UserReserved { get; set; }

    public Guid UserId { get; set; }

    public string Day { get; set; }

    public string Hours { get; set; }

    public string authority { get; set; }

    public bool IsInstallment { get; set; } = false;
    
}