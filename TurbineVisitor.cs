using System.Collections.Generic;
using Antlr4.Runtime;


public class TurbineToJsonVisitor : TurbineBaseVisitor<object>
{
    public override object VisitDefect(TurbineParser.DefectContext context)
    {
        Dictionary<string, object> defectData = new Dictionary<string, object>();

        // Get the defect description and details
        string description = (string)Visit(context.defectDescription());
        Dictionary<string, object> details = (Dictionary<string, object>)Visit(context.defectDetails());

        defectData.Add("Description", description);
        defectData.Add("Details", details);

        return defectData;
    }

    public override object VisitDefectDescription(TurbineParser.DefectDescriptionContext context)
    {
        // Handle the defect description (either string or text) and return it
        return context.GetText().Trim();
    }

    public override object VisitDefectDetails(TurbineParser.DefectDetailsContext context)
    {
        Dictionary<string, object> details = new Dictionary<string, object>();

        foreach (var section in context.detailSection())
        {
            var keyValuePair = (KeyValuePair<string, object>)Visit(section);
            details.Add(keyValuePair.Key, keyValuePair.Value);
        }

        return details;
    }

    public override object VisitKeyValuePair(TurbineParser.KeyValuePairContext context)
    {
        string key = context.key().GetText();
        object value = Visit(context.value());

        return new KeyValuePair<string, object>(key, value);
    }

    public override object VisitValue(TurbineParser.ValueContext context)
    {
        // Handle values (string, text, number, etc.) and return them accordingly
        if (context.STRING() != null)
        {
            return context.STRING().GetText().Trim('"');
        }
        else if (context.TEXT() != null)
        {
            return context.TEXT().GetText();
        }
        else if (context.NUMBER() != null)
        {
            return int.Parse(context.NUMBER().GetText());
        }
        else if (context.DATE() != null)
        {
            // Handle date format and return it accordingly
            // You can parse the date here and return it in the desired format
            // For simplicity, we'll return the date as a string for now
            return context.DATE().GetText();
        }
        else if (context.TIME() != null)
        {
            // Handle time format and return it accordingly
            // You can parse the time here and return it in the desired format
            // For simplicity, we'll return the time as a string for now
            return context.TIME().GetText();
        }

        return null;
    }
}
