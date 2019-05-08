using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Aml.Engine.AmlObjects;

namespace Iodd2AmlConverter.Library
{
    public class PackageHandler
    {
        public string PathToAml { get; set; }
        public string PathToIodd { get; set; }
        private List<string> PathsToImages { get; set; }

        public PackageHandler()
        {
            PathsToImages = new List<string>();
        }
        public void CreatePackage(string inputPath, string outputPath)
        {
            PathToIodd = inputPath;
            PathToAml = outputPath;
            var pathToSourceFolder = Directory.GetParent(inputPath);
            var files = pathToSourceFolder.GetFiles();

            foreach (var file in files)
            {
                if (file.Extension.Contains("jpeg") || file.Extension.Contains("jpg") || file.Extension.Contains("png"))
                {
                    PathsToImages.Add(file.FullName);
                }
            }
            
            using(var container = new AutomationMLContainer(@"C:\Users\ya6sc9\Downloads\Balluff-BNI_IOL-302-000-K006\Test.amlx"))
            {
                var root = container.AddRoot(PathToAml, new Uri("/" + Path.GetFileName(PathToAml), UriKind.RelativeOrAbsolute));
                container.AddCAEXSchema(Assembly.GetExecutingAssembly().GetManifestResourceStream("Iodd2AmlConverter.Library.Resources.CAEX_ClassModel_V2.15.xsd"), new Uri("/CAEX_ClassModel_V2.15.xsd", UriKind.RelativeOrAbsolute));
                
                foreach (var pathToImage in PathsToImages)
                {
                    container.AddAnyContent(root, pathToImage, new Uri("/" + Path.GetFileName(pathToImage), UriKind.RelativeOrAbsolute));
                }

                container.AddAnyContent(root, PathToIodd, new Uri("/" + Path.GetFileName(PathToIodd), UriKind.RelativeOrAbsolute));
                container.Close();
            }
        }
    }
}