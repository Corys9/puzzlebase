using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PuzzleBase.Web.CodeBehind
{
    public class PuzzleBase : ComponentBase
    {
        [Parameter]
        public int ID { get; set; }
    }
}
