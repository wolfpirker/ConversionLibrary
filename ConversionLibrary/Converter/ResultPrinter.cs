using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ConversionLibrary.Converter.Base;

namespace ConversionLibrary.Converter
{
    public sealed class ResultPrinter
    {
        private ResultPrinter(){

        }

        public static string GetOutputStringFromResults(StringParserResult pResult, double result){
            FormattableString formattable = $"{result:n} {pResult.ToUnit.SiPrefix}{pResult.ToUnit.UnitName}";
            return formattable.ToString(CultureInfo.InvariantCulture);
        }

    }
}