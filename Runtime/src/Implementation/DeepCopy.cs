namespace UMF.Core.Implementation
{
    public static class DeepCopy
    {
        public static T DeepClone<T>(this T obj)
        {
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }
}