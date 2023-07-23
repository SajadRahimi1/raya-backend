public class NurseFamily:BaseEntity
{
    public Nurse Nurse { get; set; }
    public Guid NurseId { get; set; }
    public string Name { get; set; }="";
    public string Information { get; set; }="";
    public string KnowTime { get; set; }="";
}