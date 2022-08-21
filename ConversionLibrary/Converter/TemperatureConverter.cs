using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Exceptions;
using System.Globalization;

namespace ConversionLibrary.Converter
{
    public class TemperatureConverter : BaseConverter
    {
        private const CategoryEnum ConverterCategory = CategoryEnum.Temperature;
        private readonly IEnumerable<string> _supportedUnits = new List<string>() {"celsius", "fahrenheit"};
        public TemperatureConverter()
        {            
        }
        public TemperatureConverter(string inputValue, string targetUnit) : base(inputValue, targetUnit)
        {
        }

        public override IEnumerable<string> SupportedUnits => _supportedUnits;

        public override string GetResult(){
            double result = 0d;
            StringParserResult pResult = this.Parse(ConverterCategory);

            switch(pResult.FromUnit.UnitName){
                case "celsius":
                        if (pResult.ToUnit.UnitName.Equals("fahrenheit")){
                            if (pResult.FromUnit.Base10 != 0){
                                result = pResult.FromUnit.Value * 1.8 * Math.Pow(10, pResult.FromUnit.Base10) + 32;                            
                            }
                            else {
                                result = pResult.FromUnit.Value * 1.8 + 32;                            
                            }
                        }
                    break;
                case "fahrenheit":
                        if (pResult.ToUnit.UnitName.Equals("celsius")){
                            if (pResult.FromUnit.Base10 != 0){
                                result = (pResult.FromUnit.Value * Math.Pow(10, pResult.FromUnit.Base10 - 32)) * 5/9;                            
                            }
                            else {
                                result = (pResult.FromUnit.Value - 32) * 5/9;    
                            }
                        }
                    break;
                default:
                    throw new InvalidInputFormatException("parser failed to return valid unit name!");
            }
            if (pResult.ToUnit.Base10 != 0){
                result *= Math.Pow(10, -pResult.ToUnit.Base10);
            }

            return GetOutputStringFromResults(pResult, result);
        }
        
    }
}