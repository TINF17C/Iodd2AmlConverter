using System;
using AMLRider.Cli.Extensions;
using AMLRider.Cli.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{

    internal abstract class FakeClass
    {
        
    }
    
    [TestClass]
    public class ParserResultTest
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsOnNullAction()
        {
            var result = new NotParsedResult();
            result.WithParsed<FakeClass>(null);
        }
        
    }
    
}