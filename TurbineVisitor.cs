using Antlr4.Runtime.Misc;
using Newtonsoft.Json.Linq;
using Orsted.WindTurbine.DSL.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static TurbineParser;

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

        defectObject["defectDescription"] = context.defectDescription().STRING().ToString().Clean();
        defectObject["site"] = context.siteDefect().TEXT().ToString().Clean();
        defectObject["position"] = context.positionDefect().TEXT().ToString().Clean();
        defectObject["location"] = context.locationDefect().TEXT().ToString().Clean();

        JArray detailsArray = new JArray();

        foreach (var detailContext in context.detailsSection().detail())
        {
            var detailObject = Visit(detailContext);
            detailsArray.Add(detailObject);
        }

        defectObject["details"] = detailsArray;

        return defectObject;
    }

    public override JObject VisitDetail(TurbineParser.DetailContext context)
    {
        string attributeName = context.GetChild(0).GetText().ToLower().Clean();
        string value = context.GetChild(1).GetText();
        return new JObject { { attributeName, value } };
    }

    public override JObject VisitObjectSections(TurbineParser.ObjectSectionsContext context)
    {
        JObject jsonObject = new JObject();

        string currentComponentName = null;
        JObject currentComponent = null;

        foreach (var objectSectionContext in context.objectSection())
        {
            if (objectSectionContext.GetText().StartsWith("#"))
            {
                currentComponentName = objectSectionContext.GetChild(1).GetText();
                currentComponent = new JObject();
                jsonObject[currentComponentName] = currentComponent;
            }

            foreach (var keyValuePropertyContext in objectSectionContext.keyValueProperty())
            {
                var key = keyValuePropertyContext.TEXT(0).ToString();
                var value = keyValuePropertyContext.TEXT(1).ToString();

                if (currentComponent != null)
                {
                    currentComponent[key] = value;
                }
            }

            foreach (var childContext in objectSectionContext.child())
            {
                var childProperties = new JObject();
                var chidPropName = childContext.GetChild(1).GetText();

                foreach (var keyValuePropertyContext in childContext.keyValueProperty())
                {
                    var key = keyValuePropertyContext.TEXT(0).ToString();
                    var value = keyValuePropertyContext.TEXT(1).ToString();
                    childProperties[key] = value;
                }

                if (currentComponent != null)
                {
                    currentComponent[chidPropName] = childProperties;
                }
            }
        }

        return jsonObject;
    }






    public override JObject VisitObjectSection(TurbineParser.ObjectSectionContext context)
    {
        JObject sectionObject = new JObject();

        foreach (var childContext in context.child())
        {
            var childObject = Visit(childContext);

            // Check if the childObject is a properties section
            if (childContext.GetText().StartsWith("--"))
            {
                var propertiesObject = new JObject();
                sectionObject["Properties"] = propertiesObject;
                propertiesObject.Merge(childObject);
            }
            else
            {
                sectionObject.Merge(childObject);
            }
        }

        return sectionObject;
    }
    public override JObject VisitReporterSection(TurbineParser.ReporterSectionContext context)
    {
        JObject reportedByObject = new JObject();
        reportedByObject["reportedBy"] = context.GetChild(0).GetText().Clean();
        reportedByObject["date"] = context.GetChild(1).GetText().Clean();
        return reportedByObject;
    }

    public override JObject VisitSummarySection(TurbineParser.SummarySectionContext context)
    {
        JObject summaryObject = new JObject();
        
        return summaryObject;
    }

   
    public override JObject VisitChild(TurbineParser.ChildContext context)
    {
        JObject childObject = new JObject();

        foreach (var keyValuePropertyContext in context.keyValueProperty())
        {
            var keyValuePropertyObject = Visit(keyValuePropertyContext);
            childObject.Merge(keyValuePropertyObject);
        }

        return childObject;
    }

    public override JObject VisitKeyValueProperty(TurbineParser.KeyValuePropertyContext context)
    {
        JObject keyValuePropertyObject = new JObject();

        string key = context.TEXT(0).ToString().Clean();
        string value = context.TEXT(1).ToString().Clean();

        keyValuePropertyObject[key] = value;

        return keyValuePropertyObject;
    }



}
