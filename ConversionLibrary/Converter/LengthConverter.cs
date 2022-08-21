using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;

namespace ConversionLibrary.Converter
{
    public class LengthConverter : BaseConverter
    {
        private const CategoryEnum ConverterCategory = CategoryEnum.Length;

        // factors in direction from input unit to output unit
        private IReadOnlyDictionary<string, double> unitFactors = new Dictionary<string, double>{            
            {"inch", 1000d/25.4},
            {"foot", 1d/0.3048},
            {"meter", 1d},
            {"metre", 1d}
        };

        public LengthConverter(string inputValue, string targetUnit) : base(inputValue, targetUnit)
        {

        }

        public override string GetResult(){
            double result = 0d;
            StringParserResult pResult = this.Parse(ConverterCategory);

            result = pResult.FromUnit.Value;
            result *= Math.Pow(10,pResult.FromUnit.Base10-pResult.ToUnit.Base10);
            result /= unitFactors[pResult.FromUnit.UnitName];
            result *= unitFactors[pResult.ToUnit.UnitName];

            return GetOutputStringFromResults(pResult, result);
        }
    }
}