using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionLibrary.Converter.Exceptions
{
    public class SupportedUnitsNotSpecifiedException : ApplicationException
    {
        public SupportedUnitsNotSpecifiedException() : base(String.Format("Parser requires a list of supported units to work correctly!")) { }    
    }
}