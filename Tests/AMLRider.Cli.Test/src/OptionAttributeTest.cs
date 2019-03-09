using System;
using AMLRider.Cli.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{
    
    [TestClass]
    public class OptionAttributeTest
    {

        [TestMethod]
        public void IsShortNameSetCorrectly()
        {
            const char shortName = 'a';
            var option = new OptionAttribute(shortName, "name");
            
            Assert.AreEqual(shortName, option.ShortName);
        }
        
        [TestMethod]
        public void IsLongNameSetCorrectly()
        {
            const string longName = "Name";
            var option = new OptionAttribute('a', longName);
            
            Assert.AreEqual(longName, option.LongName);
        }
        
        [TestMethod]
        public void IsIsRequiredSetCorrectly()
        {
            const bool value = true;
            var option = new OptionAttribute('a', "name")
            {
                IsRequired = true
            };

            Assert.AreEqual(value, option.IsRequired);
        }
        
        [TestMethod]
        public void IsHelpTextSetCorrectly()
        {
            const string helpText = "SomeHelp";
            var option = new OptionAttribute('a', "name")
            {
                HelpText = helpText
            };

            Assert.AreEqual(helpText, option.HelpText);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShortNameIsNoLetterThrowsException()
        {
            const char noLetter = '1';
            new OptionAttribute(noLetter, "name");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LongNameIsNullOrWhiteSpaceThrowsException()
        {
            const string whiteSpace = " ";
            new OptionAttribute('a', whiteSpace);
        }
        
    }
    
}