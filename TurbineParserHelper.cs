
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

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
}
