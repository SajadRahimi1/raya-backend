using System.ComponentModel.DataAnnotations;

public class ClassCategoryDto
{
    [Required(ErrorMessage ="آی دی کلاس را وارد کنید")]
    public Guid ClassId { get; set; }

    public string? Title { get; set; }


    public List<string>? Hours { get; set; }


    public List<string>? Days { get; set; }

    public string? TotallHours { get; set; }

    public string? TimeHolding { get; set; }

    public string? Price { get; set; }

    public string? PrePaid { get; set; }

    public int InstallmentNumber { get; set; }=0;

    public string? InstallmentPrice { get; set; }
}