namespace UMF.Core.Implementation
{
#if NEWTONSOFT_JSON
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
#endif
}