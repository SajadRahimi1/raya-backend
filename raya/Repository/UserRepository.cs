using MongoDB.Driver;

public class UserRepository:IUserRepository{
 private readonly IMongoCollection<User> _userCollection;

 public UserRepository(IMongoDatabase mongoDatabase){
    _userCollection=mongoDatabase.GetCollection<User>("user");
 }

    public async Task CreateNewUser(User newUser)
    {
       await _userCollection.InsertOneAsync(newUser);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userCollection.Find(_=>true).ToListAsync();
        
    }
    

}