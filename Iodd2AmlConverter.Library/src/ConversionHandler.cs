using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Aml.Elements;
using Iodd2AmlConverter.Library.Iodd.Elements;

namespace Iodd2AmlConverter.Library
{
    public class ConversionHandler
    {
        public static string Convert(string ioddText, string filePath)
        {
            try
            {
                var root = XElement.Parse(ioddText);

                var device = new IODevice();
                device.Deserialize(root);

                var amlCollection = device.ToAml();
                
                if (!(amlCollection.First() is CaexFile caexFile))
                    return null;

                caexFile.FileName = Path.GetFileName(filePath);
                
                var amlRoot = caexFile.Serialize();
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