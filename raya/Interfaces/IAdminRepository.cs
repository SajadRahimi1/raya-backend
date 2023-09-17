public interface IAdminRepository
{
    Task<Admin?> getAdminByToken(string token);
    Task<CustomActionResult> addAdmin(Admin admin);
    Task<CustomActionResult> editAdmin(Admin admin);
    Task<CustomActionResult> getAllAdmin();
    Task<CustomActionResult> checkCode(string phoneNumber,string code);
}