using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orsted.WindTurbine.DSL.Extensions
{
    public static class StringExtensions
    {
        public static string Clean(this string value){
            // "\"This is another defect\""
            return value.Trim('"');
        }
        
    }
}