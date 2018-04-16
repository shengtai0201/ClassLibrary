using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web
{
    public interface IService : ICurrentUser
    {
        IOwinContext OwinContext { set; }
    }
}
