using System;
using System.Collections.Generic;
using Iodd2AmlConverter.Cli.Attributes;
using Iodd2AmlConverter.Cli.Core;
using Iodd2AmlConverter.Cli.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Cli.Test
{
    
    [TestClass]
    public class OptionMapperTest
    {

        [Verb("test", HelpText = "Test.")]
        private class TestClassValid
        {
            
            [Option('a', "option1", IsRequired = true, HelpText = "Help.")]
            public string Option1 { get; set; }
            
            [Option('b', "option2", IsRequired = false, HelpText = "Help2.")]
            public string Option2 { get; set; }
            
        }

        private static readonly string[] TestClassValidArgs = {"--option1", "value1",  "--option2", "value2"};

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapThrowsNullExceptionOnOptionProperties()
        {
            OptionMapper.Map(null, new List<KeyValuePair<string, string>>(), new object());
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MapThrowsNullExceptionOnObject()
        {
            OptionMapper.Map(new List<OptionProperty>(), new List<KeyValuePair<string, string>>(), null);
        }

        [TestMethod]
        public void ArePropertiesMappedCorrectly()
        {
            var optionProperties = typeof(TestClassValid).GetOptionProperties();

            var tokens = Tokenizer.Tokenize(TestClassValidArgs);
            var keyValuePairs = TokenPartitioner.Partition(tokens);
            
            var obj = new TestClassValid();
            OptionMapper.Map(optionProperties, keyValuePairs, obj);
            
            Assert.AreEqual("value1", obj.Option1);
            Assert.AreEqual("value2", obj.Option2);
        }
        
    }
    
}