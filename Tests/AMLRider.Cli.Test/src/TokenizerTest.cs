using AMLRider.Cli.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{
    
    [TestClass]
    public class TokenizerTest
    {
        
        [TestMethod]
        public void TokenizeLongNameSingleArgsWorks()
        {
            var args = new[] {"--name", "token", "--other", "token"};
            var resultTokenTexts = new[] {"name", "token", "other", "token"};
            
            var tokens = Tokenizer.Tokenize(args);

            var count = 0;
            foreach (var token in tokens)
                Assert.AreEqual(resultTokenTexts[count++], token.Text);
        }
        
        [TestMethod]
        public void TokenizeLongNameAssignmentArgsWorks()
        {
            var args = new[] {"--name=token", "--other=token"};
            var resultTokenTexts = new[] {"name", "token", "other", "token"};
            
            var tokens = Tokenizer.Tokenize(args);

            var count = 0;
            foreach (var token in tokens)
                Assert.AreEqual(resultTokenTexts[count++], token.Text);
        }
        
        [TestMethod]
        public void TokenizeShortNameSingleArgsWorks()
        {
            var args = new[] {"-n", "token", "-o", "token"};
            var resultTokenTexts = new[] {"n", "token", "o", "token"};
            
            var tokens = Tokenizer.Tokenize(args);

            var count = 0;
            foreach (var token in tokens)
                Assert.AreEqual(resultTokenTexts[count++], token.Text);
        }
        
        [TestMethod]
        public void TokenizeShortNameAssignmentArgsWorks()
        {
            var args = new[] {"-n=token", "-o=token"};
            var resultTokenTexts = new[] {"n", "token", "o", "token"};
            
            var tokens = Tokenizer.Tokenize(args);

            var count = 0;
            foreach (var token in tokens)
                Assert.AreEqual(resultTokenTexts[count++], token.Text);
        }
        
    }
    
}