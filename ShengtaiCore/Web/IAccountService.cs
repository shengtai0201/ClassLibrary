using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web
{
    public interface IAccountService<TUser> where TUser : IdentityUser
    {
        SignInManager<TUser> SignInManager { get; set; }
        UserManager<TUser> UserManager { get; set; }

        Task<string> FindIdByAccountAsync(string account);
    }
}
