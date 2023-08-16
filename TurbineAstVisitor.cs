using Antlr4.Runtime.Misc;
using Newtonsoft.Json.Linq;
using Orsted.WindTurbine.DSL.AST;
using System.Collections.Generic;

namespace Orsted.WindTurbine.DSL
{
    public class TurbineASTVisitor : TurbineBaseVisitor<AstNode>
    {
        public override AstNode VisitTurbine(TurbineParser.TurbineContext context)
        {
            var sections = context.section()
                .Select(VisitSection)
                .OfType<Section>()
                .ToList();

            var turbineNode = new Turbine
            {
                Sections = sections
            };

            return turbineNode;
        }

        public override AstNode VisitSection(TurbineParser.SectionContext context)
        {
            var section = new Section();

            if (context.defectSection() != null)
            {
                section.Defect = VisitDefectSection(context.defectSection()) as Defect;
            }
            else if (context.reporterSection() != null)
            {
                section.Reporter = VisitReporterSection(context.reporterSection()) as Reporter;
            }
            else if (context.detailsSection() != null)
            {
                section.Details = context.detailsSection().STRING().GetText();
            }
            else if (context.summarySection() != null)
            {
                section.Summary = context.summarySection().STRING().GetText();
            }
            else if (context.keyValueSection() != null)
            {
                section.KeyValuePairs = new Dictionary<string, string>();
                foreach (var keyValueProperty in context.keyValueSection().keyValueProperty())
                {
                    section.KeyValuePairs[keyValueProperty.TEXT(0).GetText()] = keyValueProperty.TEXT(1).GetText();
                }
            }
            else if (context.rootSection() != null)
            {
                section.RootSectionNodes = new List<RootSectionNode> { VisitRootSection(context.rootSection()) as RootSectionNode };
            }

            return section;
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
                Nested = nestedNodes
            };

            return rootSectionNode;
        }
        public override AstNode VisitNested(TurbineParser.NestedContext context)
        {
            var keyValueNodes = new List<KeyValueNode>();

            foreach (var keyContext in context.key())
            {
                var keyValueNode = new KeyValueNode
                {
                    Key = keyContext.TEXT(0).GetText(),
                    Value = keyContext.TEXT(1).GetText()
                };
                keyValueNodes.Add(keyValueNode);
            }

            foreach (var keyValueSectionContext in context.keyValueSection())
            {
                var keyValueNode = VisitKeyValueSection(keyValueSectionContext) as KeyValueNode;
                if (keyValueNode != null)
                {
                    keyValueNodes.Add(keyValueNode);
                }
            }

            var nestedNode = new NestedNode
            {
                Content = keyValueNodes.Cast<AstNode>().ToList()
            };

            return nestedNode;
        }





        public override AstNode VisitKeyValueSection(TurbineParser.KeyValueSectionContext context)
        {
            var keyValueNodes = context.keyValueProperty()
                .Select(keyValuePropertyContext => VisitKeyValueProperty(keyValuePropertyContext) as KeyValueNode)
                .ToList();

            var keyValueSectionNode = new KeyValueSectionNode
            {
                KeyValueNodes = keyValueNodes
            };

            return keyValueSectionNode;
        }

        public override AstNode VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
        {
            var keyValueNode = new KeyValueNode
            {
                Key = context.TEXT(0).GetText(),
                Value = context.TEXT(1).GetText()
            };

            return keyValueNode;
        }

        public override AstNode VisitDefectSection(TurbineParser.DefectSectionContext context)
        {
            var defect = new Defect
            {
                defectDescription = context.defectDescription().GetText(),
                site = new Site
                {
                    site = context.site(0).GetText(),
                    siteNumber = context.site(1)?.GetText()
                },
                location = new Location
                {
                    location = context.location().GetText()
                }
                // Add other properties here
            };

            return defect;
        }
        public override AstNode VisitKey(TurbineParser.KeyContext context)
        {
            var keyValueNode = new KeyValueNode
            {
                Key = context.TEXT(0).GetText(),
                Value = context.TEXT(1).GetText()
            };

            return keyValueNode;
        }


        public override AstNode VisitReporterSection(TurbineParser.ReporterSectionContext context)
        {
            var reporter = new Reporter
            {
                reportedBy = context.STRING().GetText(),
                date = context.DATE()?.GetText(),
                time = context.TIME()?.GetText()
            };

            return reporter;
        }

        // Other methods...
    }

    internal class KeyValueSectionNode : AstNode
    {
        public List<KeyValueNode> KeyValueNodes { get; set; }
    }
}
