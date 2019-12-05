using Newtonsoft.Json;
using System.Collections.Generic;

namespace PuzzleBase.Models
{
    public class JigsawConstraint : PuzzleConstraint
    {
        [JsonProperty("regions")]
        public List<List<int>> Regions { get; set; }
    }
}
