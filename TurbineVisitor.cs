using Antlr4.Runtime.Misc;
using Newtonsoft.Json.Linq;
using Orsted.WindTurbine.DSL.Extensions;

public class TurbineVisitor : TurbineBaseVisitor<JObject>
{

    public override JObject VisitDefectSection(TurbineParser.DefectSectionContext context)
{
    JObject defectObject = new JObject();

    string defectDescription = context.defectDescription().STRING().ToString().Clean();
    defectObject["defectDescription"] = defectDescription;

    defectObject["site"] = Visit(context.siteDefect());
    defectObject["position"] = Visit(context.positionDefect());
    defectObject["location"] = Visit(context.locationDefect());

    JArray detailsArray = new JArray();

    foreach (var detailContext in context.detailsSection().detail())
    {
        var detailObject = Visit(detailContext);
        detailsArray.Add(detailObject);
    }

    defectObject["details"] = detailsArray;

    // Create a root object and add the defectObject to it
    JObject rootObject = new JObject();
    rootObject["defect"] = defectObject;

    return rootObject;
}

    public override JObject VisitTurbine(TurbineParser.TurbineContext context)
    {
        JObject jsonObject = new JObject();

        foreach (var sectionContext in context.section())
        {
            var sectionObject = Visit(sectionContext);
            jsonObject.Merge(sectionObject);
        }

        return jsonObject;
    }
    public override JObject VisitSiteDefect(TurbineParser.SiteDefectContext context)
    {
        string site = context.TEXT().ToString().Clean();

        JObject siteObject = new JObject { { "site", site } };


        return siteObject;
    }

    public override JObject VisitPositionDefect(TurbineParser.PositionDefectContext context)
    {
        return new JObject { { "position", context.TEXT().ToString() } };
    }
public override JObject VisitLocationDefect(TurbineParser.LocationDefectContext context)
{
    string location = context.TEXT().ToString();
    JObject locationObject = new JObject { { "text", new JValue(location) } };
    return locationObject;
}



    public override JObject VisitDetail(TurbineParser.DetailContext context)
    {
        string attributeName = context.GetChild(0).GetText();
        string value = context.GetChild(1).GetText();

        return new JObject { { attributeName.ToLower(), value } };
    }
    public override JObject VisitKeyValueSection(TurbineParser.KeyValueSectionContext context)
    {
        Console.WriteLine("ke");
        JObject keyValueObject = new JObject();

        foreach (var keyValuePropertyContext in context.keyValueProperty())
        {
            var propertyObject = Visit(keyValuePropertyContext);
            keyValueObject.Merge(propertyObject);
        }

        return keyValueObject;
    }
     public override JObject VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
    {
        string key = context.TEXT(0).ToString();
        string value = context.TEXT(1).ToString();

        return new JObject { { key, value } };
    }
    public override JObject VisitObjectSections(TurbineParser.ObjectSectionsContext context)
    {
        JObject rootObject = new JObject();

        string name = context.NAME().GetText().Replace("--", "");
        JArray nestedArray = new JArray();

        foreach (var nestedContext in context.prop())
        {
            JObject nestedObject = VisitProp(nestedContext);
            nestedArray.Add(nestedObject);
        }

        rootObject[name] = nestedArray;

        return rootObject;
    }

    public override JObject VisitProp(TurbineParser.PropContext context)
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

    // Implement other Visit methods for remaining grammar rules...

    // Additional utility methods for cleaning and merging JSON objects if needed...
}
