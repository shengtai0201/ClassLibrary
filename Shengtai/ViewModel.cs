using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    // 僅支援淺層複製
    public abstract class ViewModel<TEntity, TViewModel>
    {
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

            return viewModel;
        }
    }
}
