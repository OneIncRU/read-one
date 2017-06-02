using Newtonsoft.Json;
using ReadOne.Application;
using System;
using System.Collections;
using System.IO;

namespace Storage.JsonEventStore
{
    public class JsonFileEventStore : IEventStore
    {
        private const string FileExtension = ".json";
        private readonly IJsonEventStoreConfig _config;

        public JsonFileEventStore(IJsonEventStoreConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _config = config;
            if (!Directory.Exists(_config.EventStoreRootDir))
            {
                throw new ArgumentException("EventStore root directory does not exists!");
            }
        }

        public IEnumerable LoadEventsFor<TAggregate>(Guid id)
        {
            var filePath = GetFilePath(id);
            if (!File.Exists(filePath))
            {
                return new ArrayList();
            }
            var events = JsonConvert.DeserializeObject<ArrayList>(File.ReadAllText(filePath));
            return events;
        }

        public void SaveEventsFor<TAggregate>(Guid id, int eventsLoaded, ArrayList newEvents)
        {
            var existingEvents = (ArrayList)LoadEventsFor<TAggregate>(id);
            if (existingEvents.Count != eventsLoaded)
            {
                throw new Exception("Concurrency conflict; cannot persist these events!");
            }
            var file = GetFilePath(id);
            existingEvents.AddRange(newEvents);
            var jsonData = JsonConvert.SerializeObject(existingEvents);
            File.WriteAllText(file, jsonData);
        }

        private string GetFilePath(Guid id)
        {
            return Path.Combine(_config.EventStoreRootDir, id.ToString("N") + FileExtension);
        }
    }
}