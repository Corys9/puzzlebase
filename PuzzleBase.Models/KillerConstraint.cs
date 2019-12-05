using Newtonsoft.Json;
using System.Collections.Generic;

namespace PuzzleBase.Models
{
    public class KillerConstraint : PuzzleConstraint
    {
        [JsonProperty("cages")]
        public List<KillerCage> Cages { get; set; }
    }
}
