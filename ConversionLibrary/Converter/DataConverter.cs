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

        private IReadOnlyDictionary<string, double> unitFactors = new Dictionary<string, double>{            
            {"bit", 8d},
            {"byte", 1d}
        };

        public DataConverter(string inputValue, string targetUnit) : base(inputValue, targetUnit)
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