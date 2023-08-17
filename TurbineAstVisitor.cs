namespace Orsted.WindTurbine.DSL
{
    public class TurbineASTVisitor : TurbineBaseVisitor<AstNode>
    {
        public override AstNode VisitTurbine(TurbineParser.TurbineContext context)
        {
            var sections = context.section()
                .Select(VisitSection)
                .OfType<SectionNode>()
                .ToList();

            var turbineNode = new TurbineNode
            {
                Sections = sections
            };

            return turbineNode;
        }

        public override AstNode VisitSection(TurbineParser.SectionContext context)
        {
            if (context.defectSection() != null)
            {
                return VisitDefectSection(context.defectSection());
            }
            else if (context.reporterSection() != null)
            {
                return VisitReporterSection(context.reporterSection());
            }
            else if (context.detailsSection() != null)
            {
                return new DetailsSectionNode { Details = context.detailsSection().STRING().GetText() };
            }
            else if (context.summarySection() != null)
            {
                return new SummarySectionNode { Summary = context.summarySection().STRING().GetText() };
            }
            else if (context.keyValueSection() != null)
            {
                var properties = context.keyValueSection().keyValueProperty()
                    .Select(VisitKeyValueProperty)
                    .OfType<KeyValuePropertyNode>()
                    .ToList();

                return new KeyValueSectionNode { Properties = properties };
            }
            else if (context.rootSection() != null)
            {
                return VisitRootSection(context.rootSection());
            }

            return null;
        }
        public override AstNode VisitRootSection(TurbineParser.RootSectionContext context)
        {
            string name = context.NAME().GetText().Replace("--", "");

            var nestedNodes = context.nested()
                .Select(VisitNested)
                .OfType<NestedNode>()
                .ToList();

            var rootSectionNode = new RootSectionNode
            {
                Name = name,
                NestedSections = nestedNodes
            };

            return rootSectionNode;
        }

        public override AstNode VisitKeyValueSection(TurbineParser.KeyValueSectionContext context)
        {
            var properties = context.keyValueProperty()
                .Select(VisitKeyValueProperty)
                .OfType<KeyValuePropertyNode>()
                .ToList();

            var keyValueSectionNode = new KeyValueSectionNode
            {
                Properties = properties
            };

            return keyValueSectionNode;
        }

        public override AstNode VisitNested(TurbineParser.NestedContext context)
        {
            var nestedContent = new List<AstNode>();

            foreach (var keyContext in context.key())
            {
                var keyNode = new KeyNode
                {
                    Key = keyContext.TEXT(0).GetText(),
                    Value = keyContext.TEXT(1).GetText()
                };
                nestedContent.Add(keyNode);
            }

            foreach (var keyValueSectionContext in context.keyValueSection())
            {
                if (VisitKeyValueSection(keyValueSectionContext) is KeyValueSectionNode keyValueSectionNode)
                {
                    nestedContent.Add(keyValueSectionNode);
                }
            }

            var nestedNode = new NestedNode
            {
               // Nested = context.NESTED().GetText(),
                Properties = nestedContent.FirstOrDefault()
            };

            return nestedNode;
        }

        public override AstNode VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
        {
            return new KeyValuePropertyNode
            {
                Key = context.TEXT(0).GetText(),
                Value = context.TEXT(1).GetText()
            };
        }

        public override AstNode VisitDefectSection(TurbineParser.DefectSectionContext context)
        {
            var defect = new DefectSectionNode
            {
                Description = context.defectDescription().GetText(),
                Site = context.site(0).GetText(),
                AtSite = context.site(0).GetText(),
                Location = context.location().GetText(),
                Properties = context.defectProperties().defectProperty()
                    .Select(VisitDefectProperty)
                    .OfType<DefectPropertyNode>()
                    .ToList()
            };

            return defect;
        }

        public override AstNode VisitDefectType(TurbineParser.DefectTypeContext context)
        {
            return new DefectTypeNode { DefectType = context.TEXT().GetText() };
        }

        public override AstNode VisitSeverity(TurbineParser.SeverityContext context)
        {
            return new SeverityNode { Severity = context.TEXT().GetText() };
        }

        public override AstNode VisitActions(TurbineParser.ActionsContext context)
        {
            return new ActionsNode { Actions = context.TEXT().GetText() };
        }

        public override AstNode VisitComment(TurbineParser.CommentContext context)
        {
            return new CommentNode { Comment = context.STRING().GetText() };
        }
    }
}
