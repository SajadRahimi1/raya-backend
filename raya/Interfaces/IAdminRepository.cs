public interface IAdminRepository
{
    Task<Admin?> getAdminByToken(string token);
    Task<CustomActionResult> login(string username,string password);
    Task<CustomActionResult> addAdmin(Admin admin);
    Task<CustomActionResult> editAdmin(Admin admin);
    Task<CustomActionResult> getAllAdmin();
    Task<CustomActionResult> checkCode(string phoneNumber,string code);
    Task<CustomActionResult> sendCode(string phoneNumber);
    Task<CustomActionResult> getRequestedNurse(int page=1);
    Task<CustomActionResult> getRequestDetail(string id);
    Task<CustomActionResult> deleteRequest(string id);
    Task<CustomActionResult> sendMessage(Message message,IFormFile? file);
    Task<CustomActionResult> getAllMessages(int page=1);
    Task<CustomActionResult> getMessages(string userId,int page=1);
    Task<CustomActionResult> getReservedClass(string classId,int page=1);

    Task<CustomActionResult> NurseUpdateUploads(AdminNurseDto nurseUploadsDto);
    
}