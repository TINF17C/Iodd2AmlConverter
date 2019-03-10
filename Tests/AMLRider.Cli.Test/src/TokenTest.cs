using AMLRider.Cli.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{
    
    [TestClass]
    public class TokenTest
    {

        [TestMethod]
        public void IsTypeSetCorrectly()
        {
            const TokenType type = TokenType.Name;
            var token = new Token(type, "test");
            
            Assert.AreEqual(type, token.Type);
        }
        
        [TestMethod]
        public void IsTextSetCorrectly()
        {
            const string text = "name";
            var token = new Token(TokenType.Name, text);
            
            Assert.AreEqual(text, token.Text);
        }
        
        [TestMethod]
        public void IsNameTokenCreatedCorrectly()
        {
            var nameToken = Token.Name("name");
            Assert.IsInstanceOfType(nameToken, typeof(NameToken));
        }
        
        [TestMethod]
        public void IsValueTokenCreatedCorrectly()
        {
            var valueToken = Token.Value("value");
            Assert.IsInstanceOfType(valueToken, typeof(ValueToken));
        }

        [TestMethod]
        public void IsNameTokenWorks()
        {
            var token = Token.Name("name");
            Assert.IsTrue(token.IsName());
        }
        
        [TestMethod]
        public void IsValueTokenWorks()
        {
            var token = Token.Value("value");
            Assert.IsTrue(token.IsValue());
        }
        
    }
    
}