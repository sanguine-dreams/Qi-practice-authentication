using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Qi_practice_authentication.Entities;
using Qi_practice_authentication.Services;


namespace Qi_practice_authentication.Helpers.JwtMiddleWear;

public class JwtMiddleWear
{
    private readonly RequestDelegate _next;
    private readonly Appsettings _appsettings;

    public JwtMiddleWear(RequestDelegate next, IOptions<Appsettings> appsettings)
    {
        _next = next;
        _appsettings = appsettings.Value;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            await attachUserToConext(context, userService, token);
        await _next(context);
    }

    public async Task attachUserToConext(HttpContext context, IUserService userService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            context.Items["User"] = await userService.GetById(userId);
        }
        catch
        {
            
        }
    }
}