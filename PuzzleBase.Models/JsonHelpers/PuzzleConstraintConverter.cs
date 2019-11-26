using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.JsonHelpers
{
    public class PuzzleConstraintConverter : JsonConverter
    {
        static readonly JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings
        {
            ContractResolver = new PuzzleConstraintResolver()
        };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PuzzleConstraint);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            return jObject["type"].Value<string>() switch
            {
                "anti-knight" => JsonConvert.DeserializeObject<AntiKnightConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                "diagonal" => JsonConvert.DeserializeObject<DiagonalConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                "jigsaw" => JsonConvert.DeserializeObject<JigsawConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                "killer" => JsonConvert.DeserializeObject<KillerConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                "sandwich" => JsonConvert.DeserializeObject<SandwichConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                "skyscraper" => JsonConvert.DeserializeObject<SkyscraperConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                "thermo" => JsonConvert.DeserializeObject<ThermoConstraint>(jObject.ToString(), SpecifiedSubclassConversion),
                _ => throw new Exception("Unknown constraint type."),
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
