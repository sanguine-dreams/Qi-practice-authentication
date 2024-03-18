using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qi_practice_authentication.Entities;
using Qi_practice_authentication.Entities.User;
using Qi_practice_authentication.Services;

namespace Qi_practice_authentication.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class AuthController : ControllerBase
{
    private IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        var response = await _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or Password is incorrect/" });

        return Ok(response);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] User userObj)
    {
        userObj.Id = 0;
        return Ok(await _userService.AddAndUpdateUser(userObj));
    }

    // PUT api/<CustomerController>/5
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, [FromBody] User userObj)
    {
        return Ok(await _userService.AddAndUpdateUser(userObj));
    }
}
