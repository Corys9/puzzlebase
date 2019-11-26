using System.Collections.Generic;
using Newtonsoft.Json;
using PuzzleBase.Models.JsonHelpers;

namespace PuzzleBase.Models
{
    public class ThermoConstraint : PuzzleConstraint
    {
        /// <summary>
        /// Note: first element of every thermo list is the bulb.
        /// </summary>
        [JsonConverter(typeof(ThermoConverter))]
        public List<List<(int Row, int Column, string Type)>> Thermos { get; set; }
    }
}
