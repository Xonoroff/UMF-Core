using System;
using MF.Core.Scripts.Core.src;

namespace Core.src.Utils
{
    public class TypeKeysProviderDefault : IKeysProvider<Type, string>
    {
        public string ProvideKey(Type value)
        {
            return value.Name;
        }
    }
}