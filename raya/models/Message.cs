public class Message : BaseEntity
{
    public String? Content { get; set; }
    public string MessageType { get; set; } = "text";
    public User User { get; set; }
    public Guid UserId { get; set; }
    public Guid? SupportId { get; set; }
    public bool IsUserSend { get; set; }
    public bool Seen { get; set; } = false;
}
