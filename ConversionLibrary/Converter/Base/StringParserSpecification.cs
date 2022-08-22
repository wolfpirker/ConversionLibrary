using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionLibrary.Converter.Base
{
    public class StringParserSpecification
    {
        // Note: si-Prefixes must be fully written        
        protected IReadOnlyDictionary<string, Int16> siPrefixBases = new Dictionary<string, Int16>{
            {"nano", -9},
            {"micro", -6},
            {"milli", -3}, 
            {"centi", -2},
            {"deci", -1},
            {"deka", 1},
            {"hecto", 2},
            {"kilo", 3},
            {"mega", 6},
            {"giga", 9},
            {"tera", 12}
        };

        // add new conversion types here, new type in CategoryEnum, then implement specific converter class
        protected IReadOnlyDictionary<CategoryEnum, List<string>> unitMatches = 
            new Dictionary<CategoryEnum, List<string>>{
                {CategoryEnum.Temperature, new List<string>{"celsius", "fahrenheit"}},
                {CategoryEnum.Length, new List<string>{"meter", "metre", "inch", "foot", "feet", "mile", "miles"}},
                {CategoryEnum.Data, new List<string>{"bit", "byte"}}
        };
    }
}