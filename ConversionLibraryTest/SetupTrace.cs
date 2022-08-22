using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit.Framework;

namespace ConversionLibraryTest
{
    // class to allow output to terminal during testing
    // more info at: https://docs.nunit.org/articles/vs-test-adapter/Trace-and-Debug.html
    [SetUpFixture]
    public class SetupTrace
    {
        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }
    }
}