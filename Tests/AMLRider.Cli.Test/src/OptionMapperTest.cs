using System;
using System.Collections.Generic;
using AMLRider.Cli.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMLRider.Cli.Test
{
    
    [TestClass]
    public class OptionMapperTest
    {

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
        
    }
    
}