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
        // method to parse string as FromUnit and ToUnit object;
        // returns StringParserResult with extracted information 
        public StringParserResult GetParserResults(string src, string targetUnit, CategoryEnum category){
            FromUnit from;
            ToUnit to, tmp;
            double value;
            
            string inputUnit;
            string trimmedInput = src.Trim().ToLower();
            string trimmedTarget = targetUnit.Trim().ToLower();
            if (trimmedInput.Contains(' ')){
                value = double.Parse(trimmedInput.Split(" ")[0], CultureInfo.InvariantCulture);
                inputUnit = trimmedInput.Split(" ")[1];
            }
            else{
                throw new InvalidInputFormatException("inner space between value and unit is missing!");
            }

            tmp = ParseUnitInfo(trimmedInput, category);
            from = new FromUnit(tmp)
            {
                Value = value
            };
            to   = ParseUnitInfo(trimmedTarget, category);

            if (CheckUnitsCompatible(inputUnit, trimmedTarget, from, to)){
                return new StringParserResult(from, to);
            }
            else{
                throw new ConversionNotSupportedException();
            }            
        }     

        private ToUnit ParseUnitInfo(string trimmedInput, CategoryEnum category) {
            string? siMatch;
            string siPrefix = "";
            Int16 base10 = 0;
            string? unit = null;

            try{
                siMatch = siPrefixBases.Keys.SingleOrDefault(prefix => trimmedInput.Contains($"{prefix}"));
                if (siMatch != null){   
                    siPrefix = siMatch;                 
                    base10 = siPrefixBases[siMatch];
                }
            }
            catch(System.InvalidOperationException){                           
                throw new InvalidInputFormatException("Several SI-Prefixes matching instead of at maximum one!");
            } 

            try{
                unit = unitMatches[category].SingleOrDefault(unit => trimmedInput.EndsWith(unit));
            }
            catch(System.InvalidOperationException){
                throw new InvalidInputFormatException("Several unit matches in input value instead of one!");
            }  

            if (String.IsNullOrEmpty(unit)) 
                throw new ConversionNotSupportedException();

            return new ToUnit(unit, base10, siPrefix);
        }

        // method to check whether given unit+si-prefix are supported
        // e.g. Kibibyte, Kelvin should throw ConversionNotSupportedException
        private static bool CheckUnitsCompatible(string inputUnit, string targetUnit, FromUnit from, ToUnit to){
            if (inputUnit.Length > from.UnitName.Length + from.SiPrefix.Length){
                throw new ConversionNotSupportedException();
            }
            if (targetUnit.Length > to.UnitName.Length + to.SiPrefix.Length){
                throw new ConversionNotSupportedException();
            }
            return true;
        }

    }
}