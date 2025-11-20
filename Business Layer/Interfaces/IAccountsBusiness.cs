using Business_Layer.Sanitizations;
using Data_Layer.Data;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface IAccountsBusiness
    {
        int GetAccountId();
        Task<int> Login(AccountLoginInfo request);
        Task<int> InsertUser(string name, string email, string password);
    }
}
