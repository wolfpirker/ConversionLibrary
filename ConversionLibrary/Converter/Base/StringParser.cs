using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Exceptions;

namespace ConversionLibrary.Converter.Base
{
    public class StringParser : StringParserSpecification
    {        
        private readonly string _input;
        private readonly string _targetUnit;
        private readonly CategoryEnum _category;

        public StringParser(string input, string targetUnit, CategoryEnum category) 
        {
            this._input = input;     
            this._targetUnit = targetUnit;
            this._category = category;       
        }  

        // method to check whether given unit+si-prefix is fully supported
        // e.g. Kibibyte, Kelvin should throw ConversionNotSupportedException
        private static bool CheckUnitCompatible(string inputUnit, string targetUnit, FromUnit from, ToUnit to){
            if (inputUnit.Length > from.UnitName.Length + from.SiPrefix.Length){
                throw new ConversionNotSupportedException();
            }
            if (targetUnit.Length > to.UnitName.Length + to.SiPrefix.Length){
                throw new ConversionNotSupportedException();
            }
            return true;
        }


        // method to parse string as FromUnit and ToUnit object;
        // returns StringParserResult with extracted information 
        public StringParserResult GetParserResults(){
            string? siMatch;
            string? unitFrom = null, unitTo = null;
            FromUnit from;
            ToUnit to;
            string siPrefixTo = "", siPrefixFrom="";
            Int16 base10From = 0, base10To = 0;
            double value;
            
            string inputUnit;
            string trimmedInput = _input.Trim().ToLower();
            string trimmedTarget = _targetUnit.Trim().ToLower();
            if (trimmedInput.Contains(' ')){
                value = double.Parse(trimmedInput.Split(" ")[0], CultureInfo.InvariantCulture);
                inputUnit = trimmedInput.Split(" ")[1];
            }
            else{
                throw new InvalidInputFormatException("inner space between value and unit is missing!");
            }
            
            try{
                siMatch =siPrefixBases.Keys.SingleOrDefault(prefix => trimmedInput.Contains($" {prefix}"));
                if (siMatch != null){   
                    siPrefixFrom = siMatch;                 
                    base10From = siPrefixBases[siMatch];
                }

                siMatch = siPrefixBases.Keys.SingleOrDefault(prefix => trimmedTarget.Contains($"{prefix}"));
                if (siMatch != null){             
                    siPrefixTo = siMatch;
                    base10To = siPrefixBases[siMatch];
                }
            }
            catch(System.InvalidOperationException){                           
                throw new InvalidInputFormatException("Several SI-Prefixes matching instead of at maximum one!");
            }  
            try{
                unitFrom = unitMatches[_category].SingleOrDefault(unit => trimmedInput.EndsWith(unit));
            }
            catch(System.InvalidOperationException){
                throw new InvalidInputFormatException("Several unit matches in input value instead of one!");
            }  
            try{                
                unitTo = unitMatches[_category].SingleOrDefault(unit => trimmedTarget.EndsWith(unit));
            }   
            catch(System.InvalidOperationException){
                throw new InvalidInputFormatException("Several unit matches in target unit instead of one!");
            }     

            if (!String.IsNullOrEmpty(unitFrom) && !String.IsNullOrEmpty(unitTo)){
                from = new FromUnit(unitFrom, base10From, siPrefixFrom, value );  
                to = new ToUnit(unitTo, base10To, siPrefixTo );
            }  
            else{
                throw new InvalidInputFormatException();
            } 

            if (CheckUnitCompatible(inputUnit, trimmedTarget, from, to)){
                return new StringParserResult(from, to);
            }
            else{
                throw new ConversionNotSupportedException();
            }
        }      
    }
}