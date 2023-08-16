using Antlr4.Runtime.Misc;
using Newtonsoft.Json.Linq;
using Orsted.WindTurbine.DSL.Extensions;

public class TurbineVisitor : TurbineBaseVisitor<JObject>
{
    public override JObject VisitTurbine(TurbineParser.TurbineContext context)
    {
        JObject jsonObject = new();

        foreach (var sectionContext in context.section())
        {
            var sectionObject = Visit(sectionContext);
            jsonObject.Merge(sectionObject);
        }

        return jsonObject;
    }
    public override JObject VisitSite(TurbineParser.SiteContext context)
    {
        string site = context.TEXT().ToString().Clean();
        string siteNumber = context.NUMBER()?.ToString().Clean();

        JObject siteObject = new JObject { { "site", site } };

        if (!string.IsNullOrEmpty(siteNumber))
        {
            siteObject.Add("siteNumber", siteNumber);
        }

        return siteObject;
    }

    public override JObject VisitLocation(TurbineParser.LocationContext context)
    {
        string location = context.GetText().Clean();
        return new JObject { { "location", location } };
    }
    public override JObject VisitDefectSection(TurbineParser.DefectSectionContext context)
    {
        JObject defectObject = new();
        string defectDescription = context.defectDescription()
                                          .TEXT()
                                          .ToString().Clean();
        defectObject["defectDescription"] = defectDescription;

        defectObject["site"] = Visit(context.site(0));
        defectObject["position"] = Visit(context.position());

        defectObject["location"] = Visit(context.location());




        foreach (var defectPropertyContext in context.defectProperties().defectProperty())
        {
            var propertyObject = Visit(defectPropertyContext);
            defectObject.Merge(propertyObject);
        }

        return new JObject { { "Defect", defectObject } };
    }

    public override JObject VisitDefectType(TurbineParser.DefectTypeContext context)
    {
        return new JObject { { "type", context.TEXT().ToString() } };
    }

    public override JObject VisitSeverity(TurbineParser.SeverityContext context)
    {
        return new JObject { { "severity", context.TEXT().ToString() } };
    }

    public override JObject VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
    {
        string key = context.TEXT()[0].GetText().ToString();
        System.Console.WriteLine(key);
        string value = context.TEXT()[0].GetText().ToString();

        return new JObject { { key, value } };
    }
    public override JObject VisitKeyValueSection(TurbineParser.KeyValueSectionContext context)
    {
        JObject keyValueObject = new JObject();

        foreach (var keyValuePropertyContext in context.keyValueProperty())
        {
            var propertyObject = Visit(keyValuePropertyContext);
            keyValueObject.Merge(propertyObject);
        }

        JObject additionalDataObject = new JObject { { "Additional Data", keyValueObject } };
        return additionalDataObject;
    }

    public override JObject VisitActions(TurbineParser.ActionsContext context)
    {
        return new JObject { { "actions", context.TEXT().ToString() } };
    }

    public override JObject VisitComment(TurbineParser.CommentContext context)
    {
        return new JObject { { "comment", context.STRING().ToString().Clean() } };
    }
    public override JObject VisitSummarySection(TurbineParser.SummarySectionContext context)
    {
        string summary = context.STRING().ToString();
        return new JObject { { "Summary", summary.Clean() } };
    }

    public override JObject VisitReporterSection(TurbineParser.ReporterSectionContext context)
    {
        string reporter = context.STRING().ToString().Clean();
        string date = context.DATE()?.ToString().Clean();
        string time = context.TIME()?.ToString().Clean();

        JObject reporterObject = new JObject
        {
            { "reportedBy", reporter }
        };

        if (!string.IsNullOrEmpty(date))
        {
            reporterObject.Add("date", date);
        }

        if (!string.IsNullOrEmpty(time))
        {
            reporterObject.Add("time", time);
        }

        return new JObject { { "Reporter", reporterObject } };
    }


     public override JObject VisitRootSection(TurbineParser.RootSectionContext context)
    {
        JObject rootObject = new JObject();

        string name = context.NAME().GetText().Replace("--","");
        JArray nestedArray = new JArray();

        foreach (var nestedContext in context.nested())
        {
            JObject nestedObject = VisitNested(nestedContext);
            nestedArray.Add(nestedObject);
        }

        rootObject[name] = nestedArray;

        return rootObject;
    }

    public override JObject VisitNested(TurbineParser.NestedContext context)
    {
        JObject nestedObject = new JObject();

        foreach (var keyContext in context.key())
        {
            string key = keyContext.TEXT(0).GetText();
            string value = keyContext.TEXT(1).GetText();
            nestedObject[key] = value;
        }

        foreach (var keyValueSectionContext in context.keyValueSection())
        {
            JObject keyValueSectionObject = VisitKeyValueSection(keyValueSectionContext);
            nestedObject.Merge(keyValueSectionObject);
        }

        return nestedObject;
    }

 




}
