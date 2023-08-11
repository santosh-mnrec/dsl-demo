using Newtonsoft.Json;

namespace Orsted.WindTurbine.DSL
{
    

    public class Program
    {
        public static void Main(string[] args)
        {
            string turbineInput = File.ReadAllText("example2.txt");

            var parserHelper = new TurbineParserHelper();
            var turbine = parserHelper.ParseTurbine(turbineInput);

            TurbineVisitor visitor = new();
            var parsedTurbine = visitor.Visit(turbine);

           var data=parserHelper.ConvertToJSON(turbineInput);
           System.Console.WriteLine(data);
        }
    }
}