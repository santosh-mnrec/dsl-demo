using Newtonsoft.Json.Linq;

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
        string site = context.TEXT().ToString();
        string siteNumber = context.NUMBER()?.ToString();

        JObject siteObject = new JObject { { "site", site } };

        if (!string.IsNullOrEmpty(siteNumber))
        {
            siteObject.Add("siteNumber", siteNumber);
        }

        return siteObject;
    }

    public override JObject VisitLocation(TurbineParser.LocationContext context)
    {
        string location = context.GetText();
        return new JObject { { "location", location } };
    }
    public override JObject VisitDefectSection(TurbineParser.DefectSectionContext context)
    {
        JObject defectObject = new();
        string defectDescription = context.defectDescription()
                                          .TEXT()
                                          .ToString();
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
        return new JObject { { "comment", context.STRING().ToString() } };
    }
    public override JObject VisitSummarySection(TurbineParser.SummarySectionContext context)
    {
        string summary = context.STRING().ToString();
        return new JObject { { "Summary", summary } };
    }

    public override JObject VisitReporterSection(TurbineParser.ReporterSectionContext context)
    {
        string reporter = context.STRING().ToString();
        string date = context.DATE()?.ToString();
        string time = context.TIME()?.ToString();

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





}
