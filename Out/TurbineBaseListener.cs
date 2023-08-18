//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.11.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from c:\temp\Orsted.WindTurbine.DSL\Turbine.g4 by ANTLR 4.11.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419


using Antlr4.Runtime.Misc;
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="ITurbineListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.11.1")]
[System.Diagnostics.DebuggerNonUserCode]
[System.CLSCompliant(false)]
public partial class TurbineBaseListener : ITurbineListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.turbine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTurbine([NotNull] TurbineParser.TurbineContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.turbine"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTurbine([NotNull] TurbineParser.TurbineContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.section"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSection([NotNull] TurbineParser.SectionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.section"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSection([NotNull] TurbineParser.SectionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.defectSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDefectSection([NotNull] TurbineParser.DefectSectionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.defectSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDefectSection([NotNull] TurbineParser.DefectSectionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.defectDescription"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDefectDescription([NotNull] TurbineParser.DefectDescriptionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.defectDescription"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDefectDescription([NotNull] TurbineParser.DefectDescriptionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.siteDefect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSiteDefect([NotNull] TurbineParser.SiteDefectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.siteDefect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSiteDefect([NotNull] TurbineParser.SiteDefectContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.positionDefect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPositionDefect([NotNull] TurbineParser.PositionDefectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.positionDefect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPositionDefect([NotNull] TurbineParser.PositionDefectContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.locationDefect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLocationDefect([NotNull] TurbineParser.LocationDefectContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.locationDefect"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLocationDefect([NotNull] TurbineParser.LocationDefectContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.detailsSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDetailsSection([NotNull] TurbineParser.DetailsSectionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.detailsSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDetailsSection([NotNull] TurbineParser.DetailsSectionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.detail"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterDetail([NotNull] TurbineParser.DetailContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.detail"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitDetail([NotNull] TurbineParser.DetailContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.timezone"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterTimezone([NotNull] TurbineParser.TimezoneContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.timezone"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitTimezone([NotNull] TurbineParser.TimezoneContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.reporterSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReporterSection([NotNull] TurbineParser.ReporterSectionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.reporterSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReporterSection([NotNull] TurbineParser.ReporterSectionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.summarySection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterSummarySection([NotNull] TurbineParser.SummarySectionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.summarySection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitSummarySection([NotNull] TurbineParser.SummarySectionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.keyValueSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterKeyValueSection([NotNull] TurbineParser.KeyValueSectionContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.keyValueSection"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitKeyValueSection([NotNull] TurbineParser.KeyValueSectionContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.keyValueProperty"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterKeyValueProperty([NotNull] TurbineParser.KeyValuePropertyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.keyValueProperty"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitKeyValueProperty([NotNull] TurbineParser.KeyValuePropertyContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.objectSections"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterObjectSections([NotNull] TurbineParser.ObjectSectionsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.objectSections"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitObjectSections([NotNull] TurbineParser.ObjectSectionsContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.prop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterProp([NotNull] TurbineParser.PropContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.prop"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitProp([NotNull] TurbineParser.PropContext context) { }
	/// <summary>
	/// Enter a parse tree produced by <see cref="TurbineParser.key"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterKey([NotNull] TurbineParser.KeyContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="TurbineParser.key"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitKey([NotNull] TurbineParser.KeyContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
