public interface IUserRepository{
    Task<List<User>> GetAllAsync();
    Task<User?> getSingleUser(string phoneNumber);
    Task<User?> getSingleUserById(string id);
    Task CreateNewUser(User phoneNumber);
    Task UpdateUser(User updatedUser);
    Task<CustomActionResult> CheckSms(string phoneNumber, string code);
    Task<CustomActionResult> GetUserClasses(string id);
    Task<CustomActionResult> GetUserNurseReserved(string id);
    Task<User?> FindUserByToken(string token);

}