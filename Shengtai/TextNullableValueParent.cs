using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class TextNullableValueParent<TText, TValue> : TextNullableValuePair<TText, TValue> where TValue : struct
    {
        public TValue Parent { get; set; }
    }
}
