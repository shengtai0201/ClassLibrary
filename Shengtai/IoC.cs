using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public class IoC
    {
        private static IContainer container = null;
        private static readonly Lazy<IContainer> lazy = new Lazy<IContainer>(() => container);
        public static IContainer Container
        {
            get
            {
                return lazy.Value;
            }
            set
            {
                container = value;
            }
        }

        public static TService Resolve<TService>() where TService : class
        {
            return Container.ResolveOptional<TService>();
        }
    }
}
