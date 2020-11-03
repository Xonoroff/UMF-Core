using System;
using System.Reflection;

namespace Core.src.Utils
{
    public static class CustomRandomizer
    {
        public static void Randomize(object o)
        {
            var allFields = o.GetType().GetFields();
            foreach (var fieldInfo in allFields)
            {
                fieldInfo.SetValue(o, GetRandomValue(fieldInfo));
            }

            var allProps = o.GetType().GetProperties();
            foreach (var propInfo in allProps)
            {
                propInfo.SetValue(o, GetRandomValue(propInfo), null);
            } 
        }

        public static object GetRandomValue(MemberInfo memberInfo)
        {
            Type expectedType = null;
            if (memberInfo is FieldInfo)
            {
                expectedType = ((FieldInfo) memberInfo).FieldType;
            }
            else if (memberInfo is PropertyInfo)
            {
                expectedType = ((PropertyInfo) memberInfo).PropertyType;
            }

            var randomValue = GetRandomValue(expectedType);
            if (expectedType == typeof(string))
            {
                randomValue += "-" + memberInfo.Name.ToLower();
            }

            return randomValue;
        }

        public static T GetRandomValue<T>()
        {
            return (T)GetRandomValue(typeof(T));
        }
        
        public static object GetRandomValue(Type expectedType)
        {
            if (expectedType.IsValueType)
            {
                if (expectedType == typeof(Guid))
                {
                    return Guid.NewGuid();
                }

                if (expectedType == typeof(int))
                {
                    return new Random().Next(int.MaxValue);
                }
                
                return Activator.CreateInstance(expectedType);
            }
            if (expectedType == typeof(string))
            {
                return "fake-string";
            }
            else
            {
                return null;
            }
        }
    }
}