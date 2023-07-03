public interface IUserRepository{
    Task<List<User>> GetAllAsync();
    Task<User?> getSingleUser(string phoneNumber);
    Task CreateNewUser(User phoneNumber);
    Task UpdateUser(User updatedUser);
}