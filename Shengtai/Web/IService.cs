using Microsoft.Owin;

namespace Shengtai.Web
{
    public interface IService : ICurrentUser
    {
        IOwinContext OwinContext { set; }
    }
}