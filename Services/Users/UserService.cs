using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Qi_practice_authentication.Data;
using Qi_practice_authentication.Entities;
using Qi_practice_authentication.Entities.User;

namespace Qi_practice_authentication.Services;

public class UserService : IUserService
{
    private readonly Appsettings _appSettngs;
    private readonly OurHeroDbContext db;

    public UserService(IOptions<Appsettings> appSettings, OurHeroDbContext _db)
    {
        _appSettngs = appSettings.Value;
        db = _db;
    }
    public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
    {
        var user = await db.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);

        if (user == null) return null;
        var token = await generateJwtToken(user);

        return new AuthenticateResponse(user, token);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await db.Users.Where(x => x.isActive == true).ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> AddAndUpdateUser(User userObj)
    {
        bool isSuccess = false;
        if (userObj.Id > 0)
        {
            var obj = await db.Users.FirstOrDefaultAsync(c => c.Id == userObj.Id);
            if (obj != null)
            {
                obj.FirstName = userObj.FirstName;
                obj.LastName = userObj.LastName;
                db.Users.Update(obj);
                isSuccess = await db.SaveChangesAsync() > 0;
            }
        }
        else
        {
            await db.Users.AddAsync(userObj);
            isSuccess = await db.SaveChangesAsync() > 0;
        }

        return isSuccess ? userObj : null;
    }

    private async Task<string> generateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_appSettngs.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });
        return tokenHandler.WriteToken(token);
    }
}