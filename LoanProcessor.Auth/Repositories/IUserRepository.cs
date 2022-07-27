using LoanProcessor.Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanProcessor.Auth.Repositories
{
    public interface IUserRepository
    {
        Task<User>  Authenticate(string userName, string password);
    }
}
