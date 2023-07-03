using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase{
    private readonly AppDbContext _appDbContext;
    public UserController( AppDbContext appDbContext)
    {
        _appDbContext=appDbContext; 
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(){
        var users =await _appDbContext.Users.ToListAsync();
        return Ok(users);
    }
    

}