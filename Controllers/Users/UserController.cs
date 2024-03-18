// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Qi_practice_authentication.Entities.User;
// using Qi_practice_authentication.Repository.Users;
//
// namespace Qi_practice_authentication.Controllers.Users;
//
// [ApiController]
// [Authorize]
// [Route("api/[controller]")]
// public class UserController : ControllerBase
// {
//     private readonly IUserRepository _userRepository;
//
//     public UserController(IUserRepository userRepository)
//     {
//         _userRepository = userRepository;
//     }
//
//     [HttpGet]
//     public IActionResult GetAll()
//     {
//         var result = _userRepository.GetAll();
//         return Ok(result);
//     }
//
//     [HttpPost]
//     public IActionResult Create([FromBody]User user)
//     {
//         var createdUser = _userRepository.Create(user);
//         return Ok(createdUser);
//     }
//
//     [HttpPut("{Id}")]
//     public IActionResult Update(int Id, [FromBody] User user)
//     {
//         var updatedUser = _userRepository.Update(Id, user);
//         if (updatedUser == null)
//             return NotFound();
//         return Ok(updatedUser);
//     }
//
//     [HttpDelete("{Id}")]
//     public IActionResult Delete(int Id)
//     {
//         _userRepository.Delete(Id);
//         return NoContent();
//     }
// }