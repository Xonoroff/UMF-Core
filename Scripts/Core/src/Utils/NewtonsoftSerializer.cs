using MF.Core.Scripts.Core.src;

namespace Core.src.Utils
{
    public class NewtonsoftSerializer<TObject> : ISerializer<string, TObject>
    {
        public string SerializeObject(TObject value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        public TObject DeserializeObject(string value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TObject>(value);
        }
    }
}