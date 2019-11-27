using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleBase.Web.Extensions
{
    public static class ListExtensions
    {
        public static List<T> Copy<T>(this List<T> original)
        {
            var result = new List<T>();

            foreach (var element in original)
                result.Add(element);

            return result;
        }
    }
}
