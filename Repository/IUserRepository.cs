public interface IUserRepository{
    Task<List<User>> GetAllAsync();
    Task CreateNewUser(User newUser);
}