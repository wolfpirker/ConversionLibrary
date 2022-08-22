using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Exceptions;

namespace ConversionLibrary.Converter.Base
{

    public abstract class BaseConverter
    {
        private string? _inputValue;
        private string? _targetUnit;  

        protected BaseConverter() 
        {
        }    

        protected BaseConverter(string inputValue, string targetUnit) 
        {
            this._inputValue = inputValue;
            this._targetUnit = targetUnit;
        }

        public abstract IEnumerable<string> SupportedUnits
        {
            get;
        }
        protected string InputValue => _inputValue;
        protected string TargetUnit => _targetUnit;

        public void SetNewInput(string inputValue, string targetUnit){
            this._inputValue = inputValue;
            this._targetUnit = targetUnit;
        }


        public abstract string GetResult();

        // method to extract value and units from the string input parameters
        // using StringParser class
        protected StringParserResult Parse(CategoryEnum category){
            var parser = new StringParser(InputValue, TargetUnit, category);            
            StringParserResult result = parser.GetParserResults();            
            return result;
        }     

        protected static string GetOutputStringFromResults(StringParserResult pResult, double result){
            FormattableString formattable = $"{result:n} {pResult.ToUnit.SiPrefix}{pResult.ToUnit.UnitName}";
            return formattable.ToString(CultureInfo.InvariantCulture);
        }
    }
}