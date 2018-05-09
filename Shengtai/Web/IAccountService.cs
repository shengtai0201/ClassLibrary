using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace Shengtai.Web
{
    public interface IAccountService<TUser> where TUser : IdentityUser
    {
        IAuthenticationManager AuthenticationManager { get; set; }
        UserManager<TUser> UserManager { get; set; }

        Task<string> FindIdByAccountAsync(string account);
    }
}