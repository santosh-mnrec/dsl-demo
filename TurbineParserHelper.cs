using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Newtonsoft.Json.Linq;

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

            var tree = parser.turbine();
            var visitor = new TurbineVisitor();
            return visitor.Visit(tree);
        }

    }
}