public interface IKavehnegarRespository
{
    Task<CustomActionResult> sendLoginSms(string phoneNumber,string code);
    Task<CustomActionResult> sendNurseReserveSms(string phoneNumber);
    Task<CustomActionResult> sendHiringNurseSms(string phoneNumber);
}