using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Shengtai
{
    public static class DefaultExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumDescription<TEnum>(this int value) where TEnum : struct
        {
            return GetEnumDescription<TEnum>(value.ToString());
        }

        public static string GetEnumDescription<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse<TEnum>(value, out TEnum result))
                return GetEnumDescription(result as Enum);

            return null;
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

        public static double Division(this int molecular, int denominator)
        {
            if (molecular == 0 || denominator == 0)
                return 0;

            return molecular * 1.0 / denominator;
        }

        public static IQueryable<TSource> Between<TSource, TKey>(this IQueryable<TSource> source, 
            Expression<Func<TSource, TKey>> keySelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression key = Expression.Invoke(keySelector, keySelector.Parameters.ToArray());
            Expression lowerBound = Expression.GreaterThanOrEqual(key, Expression.Constant(low));
            Expression upperBound = Expression.LessThanOrEqual(key, Expression.Constant(high));

            Expression and = Expression.AndAlso(lowerBound, upperBound);
            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);

            return source.Where(lambda);
        }
    }
}
