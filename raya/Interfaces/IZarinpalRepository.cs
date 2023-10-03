public interface IZarinpalRepository
{
    Task<CustomActionResult> payCourse(ReserveClass reserveClass);
    Task<CustomActionResult> payHiringNurse(string nurseId);
    Task<Microsoft.AspNetCore.Mvc.IActionResult> checkPayement(string authority,string id);
    Task<CustomActionResult> checkPayementApi(string authority, string id);
}