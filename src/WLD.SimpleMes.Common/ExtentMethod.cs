using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.Common
{
    public static class ExtentMethod
    {
        public static void CopyProperties<T, U>(this Object o, T source, U destination)
        {
            var sourceProperties = typeof(T).GetProperties();
            foreach (var prop in sourceProperties)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    var value = prop.GetValue(source, null);
                    var destinationProp = typeof(U).GetProperty(prop.Name);
                    if (destinationProp != null && destinationProp.CanWrite && destinationProp.PropertyType.IsAssignableFrom(prop.PropertyType))
                    {
                        destinationProp.SetValue(destination, value, null);
                    }
                }
            }
        }
    }
}
