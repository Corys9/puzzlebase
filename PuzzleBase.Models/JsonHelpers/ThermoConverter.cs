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
            var thermos = JArray.FromObject(value);

            writer.WriteStartArray();

            foreach (JArray thermo in thermos)
            {
                writer.WriteStartArray();

                for (int i = 0; i < thermo.Count; i++)
                {
                    var thermoElement = thermo[i] as JObject;
                    writer.WriteStartArray();
                    writer.WriteValue(thermoElement["Item1"].Value<int>());
                    writer.WriteValue(thermoElement["Item2"].Value<int>());
                    writer.WriteValue(thermoElement["Item3"].Value<string>());
                    writer.WriteEndArray();
                }

                writer.WriteEndArray();
            }

            writer.WriteEndArray();
        }
    }
}
