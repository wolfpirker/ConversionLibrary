using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Contract;

namespace ConversionLibrary.Converter
{
    public class LengthConverter : IConverter
    {
        private StringParser _parser;
        private const CategoryEnum ConverterCategory = CategoryEnum.Length;

        // factors in direction from input unit to output unit; reverse like divisor
        private static readonly IReadOnlyDictionary<string, double> unitFactors = new Dictionary<string, double>{            
            {"inch", 1000d/25.4},
            {"foot", 1d/0.3048},
            {"feet", 1d/0.3048},
            {"meter", 1d},
            {"metre", 1d},
            {"mile", 1d/1609d},
            {"miles", 1d/1609d}
        };
        private readonly IEnumerable<string> _supportedUnits = unitFactors.Keys;

        // avoid that class instance is created without parser
        private LengthConverter(){

        }

        public LengthConverter(StringParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException();
        }


        public IEnumerable<string> SupportedUnits => _supportedUnits;

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