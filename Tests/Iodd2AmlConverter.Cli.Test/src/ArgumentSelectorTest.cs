using Iodd2AmlConverter.Cli.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iodd2AmlConverter.Cli.Test
{
    
    [TestClass]
    public class ArgumentSelectorTest
    {
        
        private const string SampleArgs = "verb --option1 value1 --option2 value2";
        private static readonly string[] SampleArgsArray = {"verb", "--option1", "value1", "--option2", "value2"};

        [TestMethod]
        public void DoesSeparateArgs()
        {
            var args = ArgumentSelector.Select(SampleArgs);

            var counter = 0;
            foreach (var arg in args)
                Assert.AreEqual(SampleArgsArray[counter++], arg);
        }

    }
    
    
}