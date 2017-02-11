using System;
using System.ComponentModel;
using System.Reflection;

namespace Shengtai
{
    public static class DefaultExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static int? Plus(this int? leftValue, int? rightValue)
        {
            if (leftValue.HasValue)
            {
                if (rightValue.HasValue)
                    return leftValue.Value + rightValue.Value;
                else
                    return leftValue.Value;
            }
            else
            {
                if (rightValue.HasValue)
                    return rightValue.Value;
                else
                    return null;
            }
        }

        public static int Plus(this int? leftValue, int rightValue)
        {
            if (leftValue.HasValue)
                return leftValue.Value + rightValue;
            else
                return rightValue;
        }

        public static int Plus(this int leftValue, int? rightValue)
        {
            if (rightValue.HasValue)
                return leftValue + rightValue.Value;
            else
                return leftValue;
        }

        public static int? Minus(this int? leftValue, int? rightValue)
        {
            if (leftValue.HasValue)
            {
                if (rightValue.HasValue)
                    return leftValue.Value - rightValue.Value;
                else
                    return leftValue.Value;
            }
            else
            {
                if (rightValue.HasValue)
                    return 0 - rightValue.Value;
                else
                    return null;
            }
        }
    }
}
