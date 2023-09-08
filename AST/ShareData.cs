using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orsted.WindTurbine.DSL.AST
{
   
    public class ShareData
    {
        public string Name { get; set; }

        public JObject keyValuePairs { get; set; }
    }
}
