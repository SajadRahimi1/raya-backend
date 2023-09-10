public interface IKavehnegarRespository
{
    Task<CustomActionResult> sendLoginSms(string phoneNumber,string code);
    Task<CustomActionResult> sendNurseReserveSms(string phoneNumber,string name);
    Task<CustomActionResult> sendHiringNurseSms(string phoneNumber,string name);
}