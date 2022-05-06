using UnityEngine;

namespace MF.Core.Scripts.Core.src.Utils
{
    public class UnitySerializer<TObject> : ISerializer<string, TObject>
    {
        public string SerializeObject(TObject value)
        {
            return JsonUtility.ToJson(value);
        }

        public TObject DeserializeObject(string value)
        {
            return JsonUtility.FromJson<TObject>(value);
        }
    }
}