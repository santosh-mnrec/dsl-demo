using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orsted.WindTurbine.DSL.AST
{
    
    public class NestedNode : AstNode
    {
        public string NestedText { get; set; }
        public List<AstNode> Content { get; set; }
    }

    public class RootSectionNode : AstNode
    {
        public string Name { get; set; }
        public List<AstNode> Nested { get; set; }
    }

    internal class PropertyNode : AstNode
    {
        public string Key { get; set; }
    }

    public abstract class AstNode { }

    public class TurbineNode : AstNode
    {
        public List<SectionNode> Sections { get; set; }
        public RootSectionNode RootSectionNode { get; set; }
    }

    public abstract class SectionNode : AstNode { }

    public class DefectSectionNode : SectionNode
    {
        public string DefectDescription { get; set; }
        public SiteNode Site { get; set; }
        public string Position { get; set; }
        public LocationNode Location { get; set; }
        public List<DefectPropertyNode> DefectProperties { get; set; }
    }

    public class SiteNode : AstNode
    {
        public string SiteName { get; set; }
        public string SiteNumber { get; set; }
    }

    public class LocationNode : AstNode
    {
        public string LocationName { get; set; }
    }

    public class DefectPropertyNode : AstNode
    {
        public string PropertyType { get; set; }
        public string PropertyValue { get; set; }
    }

    public class ReporterSectionNode : SectionNode
    {
        public string ReporterName { get; set; }
        public string ReportDate { get; set; }
        public string ReportTime { get; set; }
    }

    public class DetailsSectionNode : SectionNode
    {
        public string Details { get; set; }
    }

    public class SummarySectionNode : SectionNode
    {
        public string Summary { get; set; }
    }

    public class KeyValueSectionNode : SectionNode
    {
        public List<KeyValuePropertyNode> KeyValueProperties { get; set; }
    }

    public class KeyValuePropertyNode : AstNode
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
