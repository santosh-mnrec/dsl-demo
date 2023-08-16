using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Orsted.WindTurbine.DSL.AST
{

    public class RootSectionNode : AstNode
    {
        public string Name { get; set; }
        public List<NestedNode> Nested { get; set; }
    }

    public class NestedNode : AstNode
    {
     
        public List<AstNode> Content { get; set; }
    }

    public class KeyValueNode : AstNode
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public abstract class AstNode
    {

    }

    public class Turbine : AstNode
    {
        

        public List<Section> Sections { get; set; }

    }

    public class Section:AstNode
    {

        public Defect Defect { get; set; }
        public Reporter Reporter { get; set; }

        public Dictionary<string, string> KeyValuePairs { get;  set; }
        public List<RootSectionNode> RootSectionNodes{get;set;}
        public string Details { get;  set; }
        public string Summary { get;  set; }
    }

    public class Reporter : AstNode
    {
        public string reportedBy { get; set; }
        public string date { get; set; }
        public string time { get; set; }

    }

    public class Defect : AstNode
    {
        public Location location{get;set;}

        public string defectDescription { get; set; }
        public Site site { get; set; }
        
    }
    public class Site
    {
        public string site { get; set; }
        public string siteNumber { get; set; }
    }
    public class Location
    {
        public string location { get; set; }
    }

}