using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;
using ConversionLibrary.Converter.Exceptions;
using System.Globalization;
using ConversionLibrary.Converter.Contract;

namespace ConversionLibrary.Converter
{
    public class TemperatureConverter : IConverter
    {
        private StringParser _parser;
        private const CategoryEnum ConverterCategory = CategoryEnum.Temperature;
        private readonly IEnumerable<string> _supportedUnits = new List<string>() {"celsius", "fahrenheit"};

        private TemperatureConverter()
        {            
        }
        public TemperatureConverter(StringParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException();
        }

        public IEnumerable<string> SupportedUnits => _supportedUnits;

        public string GetResult(string source, string targetUnit){
            double result = 0d;
            StringParserResult pResult = this._parser.GetParserResults(source, targetUnit, ConverterCategory);

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

            return ResultPrinter.GetOutputStringFromResults(pResult, result);
        }
        
    }
}