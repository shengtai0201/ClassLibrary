using Shengtai.Web.Telerik;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shengtai
{
    // 僅支援淺層複製
    public abstract class ViewModel<TKey, TViewModel, TEntity> where TViewModel : ViewModel<TKey, TViewModel, TEntity>
    {
        public TKey GetKey()
        {
            foreach(PropertyInfo property in this.GetType().GetProperties())
            {
                foreach(object attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is KeyAttribute keyAttribute)
                        return (TKey)property.GetValue(this);
                }
            }

            return default(TKey);
        }

        /// <summary>
        /// 自行撰寫設定其值的演算法
        /// </summary>
        /// <param name="entity">來自資料庫的 entity</param>
        /// <returns>view model</returns>
        protected virtual TViewModel Build(TEntity entity)
        {
            return this as TViewModel;
        }

        /// <summary>
        /// 透過 Reflection 於執行期設定其值
        /// </summary>
        /// <param name="entity">來自資料庫的 entity</param>
        /// <returns>view model</returns>
        public static TViewModel NewInstance(TEntity entity)
        {
            if (entity == null)
                return default(TViewModel);

            TViewModel viewModel = Activator.CreateInstance<TViewModel>();
            var viewModelProperties = typeof(TViewModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var entityType = entity.GetType();
            foreach (var property in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType.ToString();

                var viewModelProperty = viewModelProperties.SingleOrDefault(x =>
                {
                    var innerProperty = x.PropertyType.ToString();
                    bool sameType = propertyType == innerProperty;

                    if (!sameType)
                    {
                        if (propertyType.Contains(innerProperty) && propertyType.StartsWith("System.Nullable"))
                            sameType = true;
                        else if (innerProperty.Contains(propertyType) && innerProperty.StartsWith("System.Nullable"))
                            sameType = true;
                    }

                    return x.Name == property.Name && sameType;
                });
                if (viewModelProperty != null)
                    viewModelProperty.SetValue(viewModel, propertyValue);
            }

            return viewModel.Build(entity);
        }
    }
}
