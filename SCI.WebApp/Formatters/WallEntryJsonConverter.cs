using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SCI.BL.Entities;
using SCI.BL.Factories;

namespace SCI.WebApp.Formatters
{
    public class WallEntryJsonConverter : JsonConverter
    {
        private readonly IWallEntryFactory _wallEntryFactory;

        public WallEntryJsonConverter(IWallEntryFactory wallEntryFactory)
        {
            _wallEntryFactory = wallEntryFactory;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsSubclassOf(typeof(WallEntry)) || objectType == typeof(WallEntry);
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public WallEntry Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("Type");
            return _wallEntryFactory.Create(type);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}