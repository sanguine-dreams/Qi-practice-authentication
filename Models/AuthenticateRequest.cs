using System.ComponentModel;

namespace Qi_practice_authentication.Entities;

public class AuthenticateRequest
{
    [DefaultValue("System")]
    public required string Username { get; set; }
    
    [DefaultValue("System")]
    public required string Password { get; set; }
}