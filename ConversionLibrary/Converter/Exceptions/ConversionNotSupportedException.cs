using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionLibrary.Converter.Exceptions
{
    public class ConversionNotSupportedException : ApplicationException
    {
        public ConversionNotSupportedException() : base(String.Format($"This conversion is not supported at this time!")) { }    
    }
}