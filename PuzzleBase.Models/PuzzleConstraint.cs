using Newtonsoft.Json;
using PuzzleBase.Models.JsonHelpers;

namespace PuzzleBase.Models
{
    [JsonConverter(typeof(PuzzleConstraintConverter))]
    public abstract class PuzzleConstraint
    {
        public string Type { get; set; }
    }
}
