using Microsoft.AspNetCore.Authentication;
using Qi_practice_authentication.Entities;
using Qi_practice_authentication.Entities.User;

namespace Qi_practice_authentication.Services;

public interface IUserService
{
    Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
    Task<IEnumerable<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> AddAndUpdateUser(User userObj);
}