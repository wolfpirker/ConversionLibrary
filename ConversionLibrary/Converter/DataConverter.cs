using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;

namespace ConversionLibrary.Converter
{
    public class DataConverter : BaseConverter
    {
        private const CategoryEnum ConverterCategory = CategoryEnum.Data;

        private static readonly IReadOnlyDictionary<string, double> unitFactors = new Dictionary<string, double>{            
            {"bit", 8d},
            {"byte", 1d}
        };

        private readonly IEnumerable<string> _supportedUnits = unitFactors.Keys;

        public DataConverter()
        {            
        }

        public DataConverter(string inputValue, string targetUnit) : base(inputValue, targetUnit)
        {

        }

        public override IEnumerable<string> SupportedUnits => _supportedUnits;

        public override string GetResult(){
            double result;
            StringParserResult pResult = this.Parse(ConverterCategory);

            result = pResult.FromUnit.Value;
            result *= Math.Pow(10,pResult.FromUnit.Base10-pResult.ToUnit.Base10);
            result /= unitFactors[pResult.FromUnit.UnitName];
            result *= unitFactors[pResult.ToUnit.UnitName];

            return GetOutputStringFromResults(pResult, result);
        }
    }
}