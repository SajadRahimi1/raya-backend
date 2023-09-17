public class Admin : BaseEntity
{
    public string? name { get; set; }
    public string? phoneNumber { get; set; }
    public string? token { get; set; }

    public string? code { get; set; }
    public bool isEnable { get; set; }

}