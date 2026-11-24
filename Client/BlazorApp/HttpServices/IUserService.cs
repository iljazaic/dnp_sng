using ApiContracts;

namespace BlazorApp.HttpServices;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
}
