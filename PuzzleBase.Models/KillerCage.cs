using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models
{
    public class KillerCage
    {
        [JsonProperty("sum")]
        public int? Sum { get; set; }

        [JsonProperty("boxes")]
        public List<int> Boxes { get; set; }
    }
}
