using Antlr4.Runtime.Misc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

        defectObject["defectDescription"] = context.defectDescription().STRING().ToString();
        defectObject["site"] = context.siteDefect().TEXT().ToString();
        defectObject["position"] = context.positionDefect().TEXT().ToString();
        defectObject["location"] = context.locationDefect().TEXT().ToString();

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
        string attributeName = context.GetChild(0).GetText().ToLower();
        string value = context.GetChild(1).GetText();
        return new JObject { { attributeName, value } };
    }

    public override JObject VisitObjectSections(TurbineParser.ObjectSectionsContext context)
    {
        JObject jsonObject = new JObject();

        foreach (var objectSectionContext in context.objectSection())
        {
            if (objectSectionContext.GetChild(0).GetText().StartsWith("#"))
            {
                var componentName = objectSectionContext.GetChild(1).GetText();
                var componentObject = new JObject();

                if (objectSectionContext.keyValueProperty() != null)
                {
                    var childProperties = new JObject();

                    foreach (var keyValuePropertyContext in objectSectionContext.keyValueProperty())
                    {
                        var propertyObject = Visit(keyValuePropertyContext);
                        childProperties.Merge(propertyObject);
                    }

                    componentObject["Properties"] = childProperties;
                }

                if (objectSectionContext.child() != null)
                {
                    var childProperties = new JObject();

                    foreach (var childPropertyContext in objectSectionContext.child())
                    {
                        foreach (var keyValuePropertyContext in childPropertyContext.keyValueProperty())
                        {
                            var propertyObject = Visit(keyValuePropertyContext);
                            childProperties.Merge(propertyObject);
                        }
                    }

                    componentObject.Merge(childProperties);
                }

                jsonObject[componentName] = componentObject;
            }
            else
            {
                jsonObject.Merge(Visit(objectSectionContext)); // Merge child sections
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

        string key = context.TEXT(0).ToString();
        string value = context.TEXT(1).ToString();

        keyValuePropertyObject[key] = value;

        return keyValuePropertyObject;
    }


  
}
