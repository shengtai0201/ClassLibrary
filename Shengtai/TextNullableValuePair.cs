using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class TextNullableValuePair<TText, TValue> where TValue : struct
    {
        public TText Text { get; set; }
        public Nullable<TValue> Value { get; set; }
    }
}
