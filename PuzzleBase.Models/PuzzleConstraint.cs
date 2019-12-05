using Newtonsoft.Json;
using PuzzleBase.Models.JsonHelpers;

namespace PuzzleBase.Models
{
    [JsonConverter(typeof(PuzzleConstraintConverter))]
    public abstract class PuzzleConstraint
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
