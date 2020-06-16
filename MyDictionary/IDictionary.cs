namespace MyDictionary
{
    public interface IDictionary
    {
        void Add(string key, string value);
        bool ContainsKey(string key);
        void Remove(string key);
        string this[string key] { get; set; }
    }
}