namespace Storage.JsonEventStore
{
    public interface IJsonEventStoreConfig
    {
        string EventStoreRootDir { get; }
    }

    public class JsonEventStoreConfig : IJsonEventStoreConfig
    {
        public string EventStoreRootDir { get; set; }
    }
}