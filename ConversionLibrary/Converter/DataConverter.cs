using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Contract;

namespace ConversionLibrary.Converter
{
    public class DataConverter : IConverter
    {
        private readonly StringParser _parser;
        private const CategoryEnum ConverterCategory = CategoryEnum.Data;

        private static readonly IReadOnlyDictionary<string, double> unitFactors = new Dictionary<string, double>{            
            {"bit", 8d},
            {"byte", 1d}
        };

        private static readonly IEnumerable<string> _supportedUnits = unitFactors.Keys;

        // avoid that class instance is created without parser
        private DataConverter(){

        }

        public DataConverter(StringParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException("parser required");
        }

        public static IEnumerable<string> SupportedUnits => _supportedUnits;

        public string GetResult(string source, string targetUnit){
            double result;

            StringParserResult pResult = this._parser.GetParserResults(source, targetUnit, ConverterCategory);

            result = pResult.FromUnit.Value;
            result *= Math.Pow(10,pResult.FromUnit.Base10-pResult.ToUnit.Base10);
            result /= unitFactors[pResult.FromUnit.UnitName];
            result *= unitFactors[pResult.ToUnit.UnitName];

            return ResultPrinter.GetOutputStringFromResults(pResult, result);
        }
    }
}