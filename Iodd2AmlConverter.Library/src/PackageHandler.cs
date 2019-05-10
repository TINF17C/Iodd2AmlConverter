using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Creates the output path for the AMLX package if none is specified.
        /// </summary>
        /// <param name="ioddPath">The path to the IODD file.</param>
        /// <returns>The combined path.</returns>
        private static string CreateAmlxPackageName(string ioddPath)
        {
            var fileName = Path.GetFileNameWithoutExtension(ioddPath);
            var targetDir = Directory.GetParent(ioddPath).FullName;

            return Path.Combine(targetDir, fileName + ".amlx");
        }
        
        /// <summary>
        /// Creates an AMLX package.
        /// </summary>
        /// <param name="inputPath">The path to the IODD file.</param>
        /// <param name="outputPath">The path to the AML file.</param>
        /// <param name="amlxOutputPath">The output path for the AMLX package.</param>
        public void CreatePackage(string inputPath, string outputPath, string amlxOutputPath)
        {
            PathToIodd = inputPath;
            PathToAml = outputPath;
            
            var pathToSourceFolder = Directory.GetParent(inputPath);
            // Set path for AMLX package to IODD file path if amlxOutoutPath is null.
            var pathToAmlxPackage = amlxOutputPath ?? CreateAmlxPackageName(PathToIodd);
            
            var files = pathToSourceFolder.GetFiles();

            // Deletes an existing AMLX package which would cause errors.
            if(File.Exists(pathToAmlxPackage))
                File.Delete(pathToAmlxPackage);
            
            // Gets all paths of images needed for the AMLX package.
            foreach (var file in files)
            {
                if (file.Extension.Contains("jpeg") || file.Extension.Contains("jpg") || file.Extension.Contains("png"))
                {
                    PathsToImages.Add(file.FullName);
                }
            }
            // Creates a new AutomationMLContainer
            using(var container = new AutomationMLContainer(pathToAmlxPackage))
            {
                // Sets the root of the container to the AML file, adds the schema, the iodd and the icons.
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