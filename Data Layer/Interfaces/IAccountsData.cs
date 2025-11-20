using DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Interfaces
{
    public interface IAccountsData
    {
        Task<Account> GetUserByEmail(string email);
        Task<int> InsertUser(string name, string email, string password);
    }
}
