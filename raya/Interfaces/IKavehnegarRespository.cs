public interface IKavehnegarRespository
{
    Task<CustomActionResult> sendLoginSms(string phoneNumber,string code);
}