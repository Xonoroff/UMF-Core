using System;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation
{
    public class TypeKeysProviderDefault : IKeysProvider<Type, string>
    {
        public string ProvideKey(Type value)
        {
            return value.Name;
        }
    }
}