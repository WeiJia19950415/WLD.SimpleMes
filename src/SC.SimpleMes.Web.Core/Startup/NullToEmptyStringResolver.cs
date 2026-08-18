using Abp.Dependency;
using Abp.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace SC.SimpleMes.Startup
{
    public class NullToEmptyStringResolver : AbpMvcContractResolver
    {
        public NullToEmptyStringResolver(IIocResolver iocResolver) : base(iocResolver)
        { }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            if (jsonProperty.ValueProvider != null && jsonProperty.PropertyType == typeof(string))
            {
                jsonProperty.ValueProvider = new NullToEmptyStringValueProvider(jsonProperty.ValueProvider, jsonProperty.PropertyType);
            }

            return jsonProperty;
        }

        private class NullToEmptyStringValueProvider : IValueProvider
        {
            private readonly IValueProvider _valueProvider;

            private readonly Type _propertyType;

            public NullToEmptyStringValueProvider(IValueProvider valueProvider, Type property)
            {
                _valueProvider = valueProvider;
                _propertyType = property;
            }

            public object GetValue(object target)
            {
                var result = _valueProvider.GetValue(target);
                if (result == null)
                {
                    return string.Empty;
                }

                return result;
            }

            public void SetValue(object target, object value)
            {
                _valueProvider.SetValue(target, value);
            }
        }
    }
}

