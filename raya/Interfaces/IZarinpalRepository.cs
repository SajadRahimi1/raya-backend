public interface IZarinpalRepository
{
    Task<CustomActionResult> payCourse(string classCatgoryId,string userId,bool IsInstallment);
    Task<CustomActionResult> payHiringNurse(string nurseId);
}