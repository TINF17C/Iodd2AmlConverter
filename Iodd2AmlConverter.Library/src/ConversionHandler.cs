using System;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Iodd.Elements;

namespace Iodd2AmlConverter.Library
{
    public class ConversionHandler
    {
        public static string Convert(string ioddText)
        {
            XElement amlRoot;
            try
            {
                var root = XElement.Parse(ioddText);

                var device = new IODevice();
                device.Deserialize(root);

                var amlCollection = device.ToAml();
                amlRoot = amlCollection.First().Serialize();
                
                return amlRoot.ToString();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred during conversion. The file has probably an invalid format.");
                return null;
            }
        }
    }
}