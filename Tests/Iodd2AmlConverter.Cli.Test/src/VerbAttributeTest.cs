using System;
using Iodd2AmlConverter.Cli.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{
    
    [TestClass]
    public class VerbAttributeTest
    {
        
        [TestMethod]
        public void IsNameSetCorrectly()
        {
            const string name = "name";
            var verb = new VerbAttribute(name);
            
            Assert.AreEqual(name, verb.Name);
        }
        
        [TestMethod]
        public void IsHelpTextSetCorrectly()
        {
            const string helpText = "SomeHelp";
            var verb = new VerbAttribute("name")
            {
                HelpText = helpText
            };

            Assert.AreEqual(helpText, verb.HelpText);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NameIsNullOrWhiteSpaceThrowsException()
        {
            const string whiteSpace = " ";
            new VerbAttribute(whiteSpace);
        }
        
    }
    
}