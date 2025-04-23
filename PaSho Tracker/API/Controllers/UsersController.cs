using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.API.Controllers;


[Authorize(Roles="Admin")]
[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService,ILogger<UsersController> logger) : base(logger)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return HandleError(ex);
        }
    }

    [HttpGet("by-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        try
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
           return HandleError(ex);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            var delete = await _userService.DeleteUserById(id);
            if (!delete) return NotFound();
            return NoContent();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    [HttpPut("roles/assign")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignRole([FromQuery] string email,[FromQuery] string role)
    {
        try
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) return NotFound();
            var success = await _userService.AssignRole(email, role);
            if (!success) return BadRequest("Role already assigned or user not found");
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    [HttpDelete("roles/revoke")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RevokeRole([FromQuery] string email, [FromQuery] string role)
    {
        try
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null) return NotFound();
            var success = await _userService.RemoveRoleByEmail(email, role);
            if (!success) return BadRequest("Role not assigned or user not found");
            return NoContent();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    
    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
    {
        try
        {
            var success = await _userService.ChangePassword(model);
            if (!success) return NotFound();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

}