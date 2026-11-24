using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public AuthController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost("login")]
    public ActionResult<UserDto> Login([FromBody] LoginRequest request)
    {
        User? user = userRepo.GetMany().SingleOrDefault(u => u.UserName == request.UserName);
        if (user is null || user.Password != request.Password)
        {
            return Unauthorized("Invalid user name or password.");
        }

        return Ok(DtoMapper.ToDto(user));
    }
}
