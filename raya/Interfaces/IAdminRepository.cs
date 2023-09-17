public interface IAdminRepository
{
    Task<Admin?> getAdminByToken(string token);
}