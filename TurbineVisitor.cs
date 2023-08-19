using Antlr4.Runtime.Misc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class TurbineVisitor : TurbineBaseVisitor<JObject>
{
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


    public override JObject VisitDefectSection(TurbineParser.DefectSectionContext context)
    {
        JObject defectObject = new JObject();

        string defectDescription = context.defectDescription().STRING().ToString();
        defectObject["defectDescription"] = defectDescription;

        defectObject["site"] = context.siteDefect().Accept(this);
        defectObject["position"] = context.positionDefect().Accept(this);
        defectObject["location"] = context.locationDefect().Accept(this);

        JArray detailsArray = new JArray();

        foreach (var detailContext in context.detailsSection().detail())
        {
            var detailObject = detailContext.Accept(this);
            detailsArray.Add(detailObject);
        }

        defectObject["details"] = detailsArray;

        return new JObject { { "defect", defectObject } };
    }

    public override JObject VisitSiteDefect(TurbineParser.SiteDefectContext context)
    {
        string site = context.TEXT().ToString();
        return new JObject { { "site", site } };
    }

    public override JObject VisitPositionDefect(TurbineParser.PositionDefectContext context)
    {
        return new JObject { { "position", context.TEXT().ToString() } };
    }

    public override JObject VisitLocationDefect(TurbineParser.LocationDefectContext context)
    {
        string location = context.TEXT().ToString();
        return new JObject { { "location", location } };
    }

    public override JObject VisitDetail(TurbineParser.DetailContext context)
    {
        string attributeName = context.GetChild(0).GetText().ToLower();
        string value = context.GetChild(1).GetText();
        return new JObject { { attributeName, value } };
    }



    public override JObject VisitObjectSections(TurbineParser.ObjectSectionsContext context)
    {
        JObject jsonObject = new JObject();

        foreach (var objectSectionContext in context.children)
        {
            var sectionObject = Visit(objectSectionContext);
            jsonObject.Merge(sectionObject);
        }

        return jsonObject;
    }

    public override JObject VisitObjectSection(TurbineParser.ObjectSectionContext context)
    {
        return VisitChildren(context);
    }





    // public override JObject VisitTree(TurbineParser.TreeContext context)
    // {
    //     JObject treeObject = new JObject();

    //     foreach (var subSectionContext in context.subSection())
    //     {
    //         var subSectionName = subSectionContext.TEXT().GetText();
    //         var subSectionProperties = Visit(subSectionContext);
    //         treeObject.Add(subSectionName, subSectionProperties);
    //     }

    //     foreach (var propertiesContext in context.properties())
    //     {
    //         var propertiesObject = Visit(propertiesContext);
    //         treeObject.Merge(propertiesObject);
    //     }

    //     return treeObject;
    // }
    public override JObject VisitTree(TurbineParser.TreeContext context)
{
    JObject treeObject = new JObject();
    var subSectionName = context.GetChild(0).GetText();

    foreach (var childContext in context.children)
    {
        if (childContext is TurbineParser.PropertiesContext propertiesContext)
        {
            // If it's properties, visit it and merge with subSectionObject
            var propertiesObject = VisitProperties(propertiesContext);
            var subSectionObject = new JObject { { subSectionName, propertiesObject } };
            treeObject.Merge(subSectionObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
        }
        else if (childContext is TurbineParser.SubSectionContext subSectionContext)
        {
            // If it's a subSection, visit it
            var subSectionObject = VisitSubSection(subSectionContext);
            treeObject.Merge(subSectionObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
        }
    }

    return treeObject;
}



    public override JObject VisitProperties(TurbineParser.PropertiesContext context)
    {
        JObject propertiesObject = new JObject();

        foreach (var childContext in context.child())
        {
            var childObject = Visit(childContext);
            propertiesObject.Merge(childObject);
        }

        foreach (var subChildContext in context.subChild())
        {
            var subChildObject = Visit(subChildContext.keyValueSection());
            propertiesObject.Add(subChildContext.GetText(), subChildObject);
        }

        return propertiesObject;
    }


    public override JObject VisitSubSection(TurbineParser.SubSectionContext context)
    {
        JObject subSectionObject = new JObject();

        if (context.TEXT() != null)
        {
            string text = context.TEXT().GetText();
            subSectionObject.Add(text, new JObject());
        }

        return subSectionObject;
    }


    public override JObject VisitChild(TurbineParser.ChildContext context)
    {
        return Visit(context.keyValueSection());
    }

    public override JObject VisitSubChild(TurbineParser.SubChildContext context)
    {
        return Visit(context.keyValueSection());
    }

    public override JObject VisitKeyValueSection(TurbineParser.KeyValueSectionContext context)
    {
        JObject keyValueSectionObject = new JObject();

        foreach (var keyValuePropertyContext in context.keyValueProperty())
        {
            var keyValuePropertyObject = Visit(keyValuePropertyContext);
            keyValueSectionObject.Merge(keyValuePropertyObject);
        }

        return keyValueSectionObject;
    }
    public override JObject VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
    {
        JObject keyValuePropertyObject = new JObject();

        string key = context.TEXT(0).GetText();
        string value = context.TEXT(1).GetText();

        keyValuePropertyObject.Add(key, value);

        return keyValuePropertyObject;
    }




}
