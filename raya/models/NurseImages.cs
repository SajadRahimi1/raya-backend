public class NurseImages : BaseEntity
{
    public Nurse Nurse { get; set; }
    public Guid NurseId { get; set; }
    public string Picture { get; set; }
    public string FirstPageImage { get; set; }
    public string DescriptionImage { get; set; }
    public string AgreementImage { get; set; }
}