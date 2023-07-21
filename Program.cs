using Newtonsoft.Json;

namespace Orsted.WindTurbine.DSL
{
    

    public class Program
    {
        public static void Main(string[] args)
        {
            string turbineInput = File.ReadAllText("input.txt");

            var parserHelper = new TurbineParserHelper();
            var turbine = parserHelper.ParseTurbine(turbineInput);

            TurbineToJsonVisitor visitor = new();
            var parsedTurbine = visitor.Visit(turbine);

            string json = JsonConvert.SerializeObject(parsedTurbine, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}