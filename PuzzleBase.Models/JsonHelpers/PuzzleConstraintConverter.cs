using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.JsonHelpers
{
    public class PuzzleConstraintConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // subclasses should not use this converter
            return objectType == typeof(PuzzleConstraint);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var type = jObject["type"].Value<string>() switch
            {
                "anti-knight" => typeof(AntiKnightConstraint),
                "diagonal" => typeof(DiagonalConstraint),
                "jigsaw" => typeof(JigsawConstraint),
                "killer" => typeof(KillerConstraint),
                "sandwich" => typeof(SandwichConstraint),
                "skyscraper" => typeof(SkyscraperConstraint),
                "thermo" => typeof(ThermoConstraint),
                _ => throw new Exception("Unknown constraint type."),
            };

            // avoid infinite recursion
            var contract = serializer.ContractResolver.ResolveContract(type);
            if (contract.Converter != null &&
                !contract.Converter.GetType().IsAbstract &&
                contract.Converter.GetType() == GetType())
            {
                contract.Converter = null;
            }
         
            var jTokenReader = new JTokenReader(jObject);
            var result = serializer.Deserialize(jTokenReader, type);

            return result;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // this converter should never write, it's only used for deserialization of abstract PuzzleConstraint class
            throw new NotImplementedException();
        }
    }
}
