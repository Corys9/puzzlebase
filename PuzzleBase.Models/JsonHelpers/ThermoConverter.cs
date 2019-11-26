using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace PuzzleBase.Models.JsonHelpers
{
    public class ThermoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof((int Row, int Column, string Type)) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jArray = JArray.Load(reader);
            var thermos = new List<List<(int Row, int Column, string Type)>>();

            foreach (var thermoArray in jArray.Children<JArray>())
            {
                var thermo = new List<(int Row, int Column, string Type)>();

                foreach (var termoElementArray in thermoArray.Children<JArray>())
                {
                    var thermoElement = (
                        termoElementArray[0].ToObject<int>(),
                        termoElementArray[1].ToObject<int>(),
                        termoElementArray[2].ToObject<string>()
                    );
                    thermo.Add(thermoElement);
                }

                thermos.Add(thermo);
            }

            return thermos;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
