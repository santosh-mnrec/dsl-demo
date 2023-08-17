public abstract class AstNode { }

public class TurbineNode : AstNode
{
    public List<SectionNode> Sections { get; set; }
}

public class SectionNode : AstNode
{
    // Common properties or methods for all sections
}

public class RootSectionNode : SectionNode
{
    public string Name { get; set; }
    public List<NestedNode> NestedSections { get; set; }
}

public class NestedNode : SectionNode
{
    public List<AstNode> Properties { get; set; }
}

public class KeyNode : AstNode
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public class DefectSectionNode : SectionNode
{
    public string Description { get; set; }
    public string Site { get; set; }
    public string Location { get; set; }
    public string AtSite { get; set; }
    public List<DefectPropertyNode> Properties { get; set; }
}

public abstract class DefectPropertyNode : AstNode { }

public class DefectTypeNode : DefectPropertyNode
{
    public string DefectType { get; set; }
}

public class SeverityNode : DefectPropertyNode
{
    public string Severity { get; set; }
}

public class ActionsNode : DefectPropertyNode
{
    public string Actions { get; set; }
}

public class CommentNode : DefectPropertyNode
{
    public string Comment { get; set; }
}

public class ReporterSectionNode : SectionNode
{
    public string Reporter { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
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
    public List<KeyValuePropertyNode> Properties { get; set; }
}

public class KeyValuePropertyNode : AstNode
{
    public string Key { get; set; }
    public string Value { get; set; }
}
