

using Options;
using System.Security.Claims;
using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Business_Layer.Sanitizations;
using Data_Layer.Data;


namespace Business_Layer.Business;

public class AccountsBusiness 
{
    private IHttpContextAccessor _httpContextAccessor { get; }
    public AccountsData AccountsData { get; }

    private HttpContext HttpContext => _httpContextAccessor.HttpContext;

    public AccountsBusiness(IHttpContextAccessor httpContext, AccountsData accountsData)
    {
        _httpContextAccessor = httpContext;
        AccountsData = accountsData;
    }


    public int GetAccountId()
    {
        try
        {
            var claimIdentity = HttpContext.User.Identity as ClaimsIdentity;
            return int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public async Task<int> Login(AccountLoginInfo request)
    {
        var account = await AccountsData.GetUserByEmail(request.email);

        if (!IsSamePassword(request.password, account.password))
        {
            return 0;
        }

        return account.id;
    }


    public async Task<int> InsertUser(string name, string email, string password)
    {
        string Sanitizedpassword = Sanitization.SanitizeInput(password);

        if (Sanitizedpassword != password)
        {
            return 0;
        }

        var passwordHasher = new PasswordHasher<object>();
        string hashedPassword = passwordHasher.HashPassword(null, password);

        return await AccountsData.InsertUser(name, email, hashedPassword);
    }


    private bool IsSamePassword(string password, string hashedPassword)
    {
        var passwordHasher = new PasswordHasher<object>();
        var isValidPassword = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
        return isValidPassword == PasswordVerificationResult.Success;
    }

}
