using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SCI.BL.Entities;
using SCI.BL.Factories;

namespace SCI.App.Adapters.DataAccess
{
    public class JsonWallEntryConverter : CustomCreationConverter<WallEntry>
    {
        private readonly WallEntryFactory _wallEntryFactory = new WallEntryFactory();

        public override WallEntry Create(Type objectType)
        {
            //Wall entry is abstract
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