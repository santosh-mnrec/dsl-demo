using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orsted.WindTurbine.DSL.AST;

namespace Orsted.WindTurbine.DSL
{
    public class TurbineParserHelper
    {
        public IParseTree ParseTurbine(string input)
        {
            AntlrInputStream inputStream = new AntlrInputStream(input);
            TurbineLexer lexer = new TurbineLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            TurbineParser parser = new TurbineParser(commonTokenStream);
            return parser.turbine();


        }
        public JObject ConvertToJSON(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new TurbineLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new TurbineParser(tokenStream);
            var parseTree = parser.turbine();

            var visitor = new TurbineVisitor();
            var astVisitor = new TurbineASTVisitor();
            AstNode ast = astVisitor.VisitTurbine(parseTree);
            System.Console.WriteLine(JsonConvert.SerializeObject(ast,Formatting.Indented));

            return visitor.Visit(parseTree);
        }

    }
}