using Business_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Presentation_Layer.Authentication;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using Microsoft.Identity.Client;

namespace Presentation_Layer.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    public IAccountsBusiness AccountsBusiness { get; }
    public AuthenticateHelper AuthenticateHelper { get; }

    public AccountController(IAccountsBusiness accountsBusiness, AuthenticateHelper authenticateHelper)
    {
        AccountsBusiness = accountsBusiness;
        AuthenticateHelper = authenticateHelper;
    }



    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUser(AccountLoginInfo data)
    {
        int accountId = await AccountsBusiness.Login(data);

        string token = await AuthenticateHelper.CreateToken(accountId);

        return !string.IsNullOrEmpty(token) ?
            Ok(token) : BadRequest();
    }



    [AllowAnonymous]
    [HttpPost("register/{username}")]
    public async Task<IActionResult> SignInUser(string username, AccountLoginInfo request)
    {
        string inValidResult = await AuthenticateHelper.IsValidAuthenticate(request);

        if (!string.IsNullOrEmpty(inValidResult))
        {
            return BadRequest(inValidResult);
        }

        int accountId = await AccountsBusiness.InsertUser(username, request.email, request.password);

        string token = await AuthenticateHelper.CreateToken(accountId);

        return !string.IsNullOrEmpty(token) ?
            Ok(token) : BadRequest();
    }


}