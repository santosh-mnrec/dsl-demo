using Newtonsoft.Json.Linq;
using Orsted.WindTurbine.DSL.Extensions;

public class TurbineVisitor : TurbineBaseVisitor<JObject>
{
    public override JObject VisitTurbine(TurbineParser.TurbineContext context)
    {
        JObject jsonObject = new JObject();

        foreach (var sectionContext in context.section())
        {
            var sectionObject = Visit(sectionContext);
            jsonObject.Merge(sectionObject, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
        }


        return jsonObject;
    }
    public override JObject VisitDefectStatement(TurbineParser.DefectStatementContext context)
    {
        JObject defectObject = new JObject();

        // Check if there's a reporterClause and add it to the defectObject
        if (context.reporterClause() != null)
        {
            defectObject["Turbine"] = new JObject
            {
                ["reporter"] = context.reporterClause().STRING().ToString().Clean()
            };
        }

        // Handle the rest of the defectBlock properties
        JObject defectBlockObject = new JObject();
        foreach (var propertyContext in context.defectBlock().children)
        {

            if (propertyContext is TurbineParser.DescriptionPropertyContext)
            {
                defectBlockObject["Description"] = propertyContext.GetChild(1).ToString().Clean();
            }
            else if (propertyContext is TurbineParser.SitePropertyContext)
            {
                defectBlockObject["Site"] = propertyContext.GetChild(1).ToString().Clean();
            }
            else if (propertyContext is TurbineParser.PositionPropertyContext)
            {
                defectBlockObject["Position"] = propertyContext.GetChild(1).ToString().Clean();
            }
            else if (propertyContext is TurbineParser.LocationPropertyContext)
            {
                defectBlockObject["Location"] = propertyContext.GetChild(1).ToString().Clean();
            }
            else if (propertyContext is TurbineParser.DatePropertyContext)
            {
                defectBlockObject["Date"] = propertyContext.GetChild(1).ToString().Clean();
            }
            else if (propertyContext is TurbineParser.TimePropertyContext)
            {
                defectBlockObject["Time"] = propertyContext.GetChild(1).ToString().Clean();
            }
            else if (propertyContext is TurbineParser.DetailsPropertyContext detailsPropertyContext)
            {
                // Handle the Details property
                JObject detailsObject = new JObject();
                foreach (var detailContext in detailsPropertyContext.detailProperty())
                {
                    string propertyName = detailContext.GetChild(0).GetText();
                    string propertyValue = detailContext.GetChild(1).GetText().Clean();
                    detailsObject[propertyName] = propertyValue;
                }
                defectBlockObject["Details"] = detailsObject;
            }



        }

        defectObject["Turbine"]["Defect"] = defectBlockObject;

        return defectObject;
    }

    public override JObject VisitDetailProperty(TurbineParser.DetailPropertyContext context)
    {
        string propertyName = context.GetChild(0).GetText();
        string propertyValue = context.GetChild(1).GetText();

        JObject detailPropertyObject = new JObject
    {
        { propertyName, propertyValue }
    };

        return detailPropertyObject;
    }



    public override JObject VisitSection(TurbineParser.SectionContext context)
    {
        JObject sectionObject = new JObject();

        // Check if the section contains a defectStatement and handle it
        if (context.defectStatement() != null)
        {
            sectionObject.Merge(Visit(context.defectStatement()));
        }

        // Check if the section contains a keyValueSection and handle it
        if (context.keyValueSection() != null)
        {
            sectionObject.Merge(Visit(context.keyValueSection()));
        }

        // Check if the section contains objectSections and handle it
        if (context.objectSections() != null)
        {
            sectionObject.Merge(Visit(context.objectSections()));
        }


        return sectionObject;
    }
    public override JObject VisitReporterClause(TurbineParser.ReporterClauseContext context)
    {
        JObject reporterObject = new JObject();
        reporterObject["Reporter"] = context.STRING().ToString().Clean();
        return reporterObject;
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
                    var key = keyValuePropertyContext.TEXT(0).ToString().Clean();
                    childProperties[key] = keyValuePropertyContext.TEXT(1).ToString().Clean();
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
