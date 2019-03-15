using System;
using System.IO;
using System.Xml.Linq;
using AMLRider.Cli;
using AMLRider.Cli.Attributes;
using AMLRider.Cli.Extensions;
using AMLRider.Cli.Helpers;
using AMLRider.Library;
using AMLRider.Library.Rules;

namespace AMLRider
{
    [Verb("convert", HelpText = "Converts an IODD file to an AML file.")]
    public class ConvertOptions
    {
        [Option('f', "file", IsRequired = true, HelpText = "The IODD file to convert.")]
        public string File { get; set; }

        [Option('o', "output", IsRequired = false, HelpText = "Specifies the output file path.")]
        public string Output { get; set; }
    }

    public static class Program
    {

        private static bool ShouldOverride(string file)
        {
            var shouldOverride = true;
            if (!File.Exists(file)) 
                return true;
            
            Console.WriteLine($"The file {file} does exist already.");
            Console.Write("Do you want to override it?[Y/n]");
            var key = Console.ReadKey().KeyChar;

            if (key != 'y' || key != 'Y')
                shouldOverride = false;

            return shouldOverride;
        }
        
        private static void OnConvertOptionsParsed(ConvertOptions options)
        {
            Console.WriteLine(options.File);
            Console.WriteLine(options.Output);

            if (!File.Exists(options.File))
            {
                Console.WriteLine($"The file {options.File} does not exist.");
                return;
            }

            string fileText;
            try
            {
                fileText = File.ReadAllText(options.File);
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Unable to read file. Maybe you are missing permissions or the file is opened in another software.");
                return;
            }

            var handler = new ConversionHandler();
            string convertedXml;
            
            try
            {
                convertedXml = handler.Convert(fileText);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred during conversion. The file has probably an invalid format.");
                return;
            }

            if (options.Output != null)
            {
                try
                {
                    if (!ShouldOverride(options.Output))
                        return;

                    File.WriteAllText(options.Output, convertedXml);
                }
                catch (IOException)
                {
                    Console.WriteLine(
                        "Unable to write output file. Maybe you are missing permissions or the file is opened in another software");
                    return;
                }
            }
            else
            {
                var fileName = Path.GetFileNameWithoutExtension(options.File);
                var outputFile = fileName + ".aml";

                if (!ShouldOverride(outputFile))
                    return;

                File.WriteAllText(options.Output, convertedXml);
            }
        }

        public static void Main(string[] args)
        {
            new CommandLineParser()
                .Parse(args, typeof(ConvertOptions))
                .WithParsed<ConvertOptions>(OnConvertOptionsParsed);
        }
    }
}