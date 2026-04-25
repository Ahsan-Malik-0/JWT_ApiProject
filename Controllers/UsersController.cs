using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectAPIs.Models.DTOs;
using WebProjectAPIs.Services;

// ---------------------------------------------------------------------------------------------
// [Authorize(Roles = "admin")] // Only allow users with the "admin" role to access this controller
// ---------------------------------------------------------------------------------------------

[ApiController]
[Route("api/[controller]")]
public class UsersController(UserServices userServices) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await userServices.GetAllUsers();
        return Ok(users); // Return 200 with the list of users
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await userServices.GetUserById(id);
        if (user == null) return NotFound(); // Return 404 if the user is not found

        return Ok(user); // Return 200 with the user data
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto userDto)
    {
        if (userDto == null) return BadRequest(); // Return 400 if the request body is null

        await userServices.AddUser(userDto);
        return Ok("User added successfully"); // Return 200 with a success message
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] CreateUserDto userDto)
    {
        if (userDto == null) return BadRequest(); // Return 400 if the request body is null

        await userServices.UpdateUser(id, userDto);
        return NoContent(); // Return 204 No Content to indicate successful update without returning data
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await userServices.DeleteUser(id);
        return NoContent(); // Return 204 No Content to indicate successful deletion without returning data
    }
}

