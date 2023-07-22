using System;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;

// Define the classes representing the elements of the grammar


public class Turbine
{
    public List<Defect> Defects { get; set; }
    public Reporter Reporter { get; set; }
    public Details Details { get; set; }
    public Summary Summary { get; set; }
}

public class Defect
{
    public string Text { get; set; }
    public string Site { get; set; }
    public string Postition { get; set; }
    public string Location { get; set; }
    public string DefectType { get; set; }
    public string Severity { get; set; }
    public string Actions { get; set; }
    public string Comment { get; set; }
}

public class Reporter
{
    public string Name { get; set; }
    public DateTime? Date { get; set; }
    public TimeSpan? Time { get; set; }
}

public class Details
{
    public string Text { get; set; }
}

public class Summary
{
    public string Text { get; set; }
}

public class TurbineVisitor : TurbineBaseVisitor<Turbine>
{
      Turbine turbine = new Turbine();
    public override Turbine VisitTurbine([NotNull] TurbineParser.TurbineContext context)
    {
      

        // Visit and construct defects
        turbine.Defects = new List<Defect>();
        foreach (var defectContext in context.defect())
        {
           VisitDefect(defectContext);
            
        }

        // Visit and construct reporter if available
        if (context.reporter() != null)
        {
           VisitReporter(context.reporter());
        }

        // Visit and construct details if available
        if (context.details() != null)
        {
            VisitDetails(context.details());
        }

        // Visit and construct summary if available
        if (context.summary() != null)
        {
            VisitSummary(context.summary());
        }

        return turbine;
    }

    public override Turbine VisitDefect([NotNull] TurbineParser.DefectContext context)
    {
        Defect defect = new Defect();
        defect.Text = context.TEXT().ToString();
        defect.Site = context.site(0).ToString();
        defect.Postition = context.postition().TEXT().ToString();

        // Visit and construct optional elements
        if (context.location() != null)
        {
            defect.Location = context.location().GetText();
        }
        if (context.defectType() != null)
        {
            defect.DefectType = context.defectType().GetText();
        }
        if (context.severity() != null)
        {
            defect.Severity = context.severity().GetText();
        }
        if (context.actions() != null)
        {
            defect.Actions = context.actions().TEXT().ToString();
        }
        if (context.comment() != null)
        {
            defect.Comment = context.comment().STRING().ToString();
        }

        turbine.Defects.Add(defect);
        return turbine;
    }

    public override Turbine VisitReporter([NotNull] TurbineParser.ReporterContext context)
    {
        Reporter reporter = new Reporter();
        reporter.Name = context.STRING().ToString();

        // // Visit and construct optional elements
        // if (context.DATE() != null)
        // {
        //     string dateStr = context.DATE().ToString();
        //     reporter.Date = DateTime.ParseExact(dateStr, "MM-dd-yyyy", null);
        // }
        // if (context.TIME() != null)
        // {
        //     string timeStr = context.TIME().ToString();
        //     reporter.Time = TimeSpan.ParseExact(timeStr, "hh\\:mm", null);
        // }

        turbine.Reporter=reporter;
        return turbine;
    }

    public override Turbine VisitDetails([NotNull] TurbineParser.DetailsContext context)
    {
        Details details = new Details();
        details.Text = context.STRING().ToString();
       turbine.Details=details;
       return turbine;
    }

    public override Turbine VisitSummary([NotNull] TurbineParser.SummaryContext context)
    {
        Summary summary = new Summary();
        summary.Text = context.STRING().ToString();
        turbine.Summary=summary;
        return turbine;
    }
}
