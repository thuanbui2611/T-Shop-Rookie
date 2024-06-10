using Microsoft.AspNetCore.Mvc;
using T_Shop.Infrastructure.SharedServices.EmailService;

namespace T_Shop.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;
    private readonly IEmailService _emailService;
    private readonly string controllerPrefix = "Email";

    public EmailController(ILogger<EmailController> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    /// <summary>
    /// Send email to user.
    /// </summary>
    /// <returns>Status code of the action.</returns>
    /// <response code="200">Successfully sent the email.</response>
    /// <response code="500">There is something wrong while execute.</response>
    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailOptions emailOptions)
    {
        bool result = await _emailService.SendEmailAsync(emailOptions);
        return result ? Ok() : BadRequest();
    }
}
