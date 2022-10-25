using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversionLibrary.Converter.Exceptions
{
    public class InvalidInputFormatException : ApplicationException
    {
        public InvalidInputFormatException() : base(String.Format("This input format is not supported!")) { }    
        public InvalidInputFormatException(string detail) : base(String.Format($"Format of input strings is not supported: {detail}!")) { }    
    }
}