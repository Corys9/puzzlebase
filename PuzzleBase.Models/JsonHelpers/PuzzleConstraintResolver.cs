using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleBase.Models.JsonHelpers
{
    /// <summary>
    /// Prevent deserialization infinite recursion.
    /// </summary>
    public class PuzzleConstraintResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(PuzzleConstraint).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null;

            return base.ResolveContractConverter(objectType);
        }
    }
}
