using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionLibrary.Converter.Contract
{
    public interface IConverter
    {
        public static IEnumerable<string> SupportedUnits
        {
            get;
        }
        public string GetResult(string source, string target);
    }
}