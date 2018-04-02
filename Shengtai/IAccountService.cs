using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public interface IAccountService<TUser> where TUser : IdentityUser
    {
        UserManager<TUser> UserManager { get; set; }
        IAuthenticationManager AuthenticationManager { get; set; }

        Task<string> FindIdByAccountAsync(string account);
    }
}
