﻿using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public interface IController : ICurrentUser
    {
        IOwinContext OwinContext { set; }
    }
}
