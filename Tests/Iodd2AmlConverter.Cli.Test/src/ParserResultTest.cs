using System;
using Iodd2AmlConverter.Cli.Extensions;
using Iodd2AmlConverter.Cli.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Cli.Test
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