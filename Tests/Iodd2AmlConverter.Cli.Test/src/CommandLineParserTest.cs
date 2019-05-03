using Iodd2AmlConverter.Cli.Attributes;
using Iodd2AmlConverter.Cli.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Cli.Test
{
    
    [TestClass]
    public class CommandLineParserTest
    {

        private static readonly string[] TestArgs = {"test", "--option1", "value1", "--option2", "value2"};
        private static readonly string[] WrongTestArgs = {"--test", "--option3", "value1", "--option4", "value2"};

        [Verb("test")]
        private class TestOptions
        {
            
            [Option('o', "option1")]
            public string Option1 { get; set; }
            
            [Option('q', "option2")]
            public string Option2 { get; set; }
            
        }

        [TestMethod]
        public void ReturnsParsedResult()
        {
            var parser = new CommandLineParser();
            var result = parser.Parse(TestArgs, typeof(TestOptions));
            
            Assert.IsInstanceOfType(result, typeof(ParsedResult));
        }
        
        [TestMethod]
        public void ReturnsNotParsedResult()
        {
            var parser = new CommandLineParser();
            var result = parser.Parse(WrongTestArgs, typeof(TestOptions));
            
            Assert.IsInstanceOfType(result, typeof(NotParsedResult));
        }

    }
    
}