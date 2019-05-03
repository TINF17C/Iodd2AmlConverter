using AMLRider.Cli.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{
    
    [TestClass]
    public class HelpGeneratorTest
    {

        private const string VerbName = "test";
        private const string VerbHelpText = "This is a test.";

        private const char ShortOptionName = 'o';
        private const string LongOptionName = "option";
        private const string OptionHelpText = "This is a help (Not).";
        
        
        [Verb(VerbName, HelpText = VerbHelpText)]
        private class TestOptions
        {
        
            [Option('o', "option", IsRequired = false, HelpText = OptionHelpText)]
            public string Option { get; set; }
        
        }
        
        [TestMethod]
        public void ResultHelpTextContainsNecessaryInfo()
        {
            var help = new HelpGenerator();
            var text = help.Generate(typeof(TestOptions));
            
            Assert.IsTrue(text.Contains(VerbName));
            Assert.IsTrue(text.Contains(VerbHelpText));
            Assert.IsTrue(text.Contains(ShortOptionName.ToString()));
            Assert.IsTrue(text.Contains(LongOptionName));
            Assert.IsTrue(text.Contains(OptionHelpText));
        }
        
    }
    
}