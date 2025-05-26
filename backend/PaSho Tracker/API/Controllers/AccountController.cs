using Microsoft.AspNetCore.Mvc;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;

namespace PaSho_Tracker.API.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger ) : base(logger)
    {
        _accountService = accountService;
    }
    
    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto model)
    {
        try
        {
            var success = await _accountService.ConfirmEmail(model);
            if (!success) return BadRequest();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
    
    [HttpPost("password/reset")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        try
        {
            var success = await _accountService.ResetPassword(model);
            if (!success) return BadRequest();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }

    [HttpPost("password/forgot")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]

    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        try
        {
            var success = await _accountService.ForgotPassword(model.Email);
            if (!success) return BadRequest();
            return Ok();
        }
        catch (Exception e)
        {
            return HandleError(e);
        }
    }
}