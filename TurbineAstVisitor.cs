using Antlr4.Runtime.Misc;
using Orsted.WindTurbine.DSL.AST;

namespace Orsted.WindTurbine.DSL
{

    public class TurbineASTVisitor : TurbineBaseVisitor<AstNode>
    {
        public override AstNode VisitTurbine(TurbineParser.TurbineContext context)
        {
            var turbineNode = new TurbineNode
            {
                Sections = context.section()
                    .Select(sectionContext => VisitSection(sectionContext) as SectionNode)
                    .ToList(),
                RootSectionNode = context.section()
                    .Select(sectionContext => VisitSection(sectionContext) as RootSectionNode)
                    .FirstOrDefault(node => node != null)
            };
            return turbineNode;
        }


        public override AstNode VisitDefectSection(TurbineParser.DefectSectionContext context)
        {
            var defectNode = new DefectSectionNode
            {
                DefectDescription = context.defectDescription().GetText(),
                Site = new SiteNode
                {
                    SiteName = context.site(0).GetText(),
                    SiteNumber = context.site(1)?.GetText() // Check for null here
                },
                Location = new LocationNode
                {
                    LocationName = context.location().GetText()
                }
            };
            return defectNode;
        }
        public override AstNode VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
        {
            var keyValuePropertyNode = new KeyValuePropertyNode
            {
                Key = context.TEXT(0).GetText(),
                Value = context.TEXT(1).GetText()
            };
            return keyValuePropertyNode;
        }

        public override AstNode VisitReporterSection(TurbineParser.ReporterSectionContext context)
        {
            var reporterSectionNode = new ReporterSectionNode
            {
                ReporterName = context.STRING().GetText(),
                ReportDate = context.DATE()?.GetText(),
                ReportTime = context.TIME()?.GetText()
            };
            return reporterSectionNode;
        }
        public override AstNode VisitSection([NotNull] TurbineParser.SectionContext context)
        {
            if (context.defectSection() != null)
                return VisitDefectSection(context.defectSection());
            else if (context.reporterSection() != null)
                return VisitReporterSection(context.reporterSection());
            else if (context.detailsSection() != null)
                return VisitDetailsSection(context.detailsSection());
            else if (context.summarySection() != null)
                return VisitSummarySection(context.summarySection());
            else if (context.keyValueSection() != null)
                return VisitKeyValueSection(context.keyValueSection());
            else if (context.rootSection() != null)
                return VisitRootSection(context.rootSection());

            throw new NotImplementedException("Unhandled section type");
        }
        public override AstNode VisitKeyValueSection(TurbineParser.KeyValueSectionContext context)
        {
            var keyValueSectionNode = new KeyValueSectionNode
            {
                KeyValueProperties = context.keyValueProperty()
                    .Select(keyValuePropertyContext => VisitKeyValueProperty(keyValuePropertyContext) as KeyValuePropertyNode)
                    .ToList()
            };
            return keyValueSectionNode;
        }
        public override AstNode VisitRootSection(TurbineParser.RootSectionContext context)
        {
            var rootSectionNode = new RootSectionNode
            {
                Name = context.NAME().GetText(),
                Nested = context.nested()
                    .Select(nestedContext => VisitNested(nestedContext))
                    .ToList()
            };
            return rootSectionNode;
        }

        public override AstNode VisitKey(TurbineParser.KeyContext context)
        {
            return new PropertyNode
            {
                Key = context.TEXT(0).GetText(),

            };
        }
        public override AstNode VisitNested([NotNull] TurbineParser.NestedContext context)
        {
            var nestedNode = new NestedNode
            {
                NestedText = context.NESTED().GetText(),
                Content = context.children
                    .Skip(1) // Skip the NESTED token
                    .Select(child => child.Accept(this))
                    .Where(childNode => childNode != null)
                    .ToList()
            };
            return nestedNode;
        }


    }


}
