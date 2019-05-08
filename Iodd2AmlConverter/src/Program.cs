using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Cli;
using Iodd2AmlConverter.Cli.Attributes;
using Iodd2AmlConverter.Cli.Extensions;
using Iodd2AmlConverter.Library.Iodd.Elements;

namespace Iodd2AmlConverter
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

        private const string AmlRiderLogo =
            @"  _____          _     _ ___                     _  _____                          _            @" +
            @" |_   _|        | |   | |__ \    /\             | |/ ____|                        | |           @" +
            @"   | |  ___   __| | __| |  ) |  /  \   _ __ ___ | | |     ___  _ ____   _____ _ __| |_ ___ _ __ @" +
            @"   | | / _ \ / _` |/ _` | / /  / /\ \ | '_ ` _ \| | |    / _ \| '_ \ \ / / _ \ '__| __/ _ \ '__|@" +
            @"  _| || (_) | (_| | (_| |/ /_ / ____ \| | | | | | | |___| (_) | | | \ V /  __/ |  | ||  __/ |   @" +
            @" |_____\___/ \__,_|\__,_|____/_/    \_\_| |_| |_|_|\_____\___/|_| |_|\_/ \___|_|   \__\___|_|   @";

        private static bool ShouldOverride(string file)
        {
            var shouldOverride = true;
            if (!File.Exists(file))
                return true;

            Console.WriteLine($"The file {file} does exist already.");
            Console.Write("Do you want to override it? [Y/n]: ");
            var key = Console.ReadKey().Key;

            if (key != ConsoleKey.Y)
                shouldOverride = false;

            return shouldOverride;
        }

        private static void OnConvertOptionsParsed(ConvertOptions options)
        {
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

            XElement amlRoot;
            try
            {
                var root = XElement.Parse(fileText);

                var device = new IODevice();
                device.Deserialize(root);

                var amlCollection = device.ToAml();
                amlRoot = amlCollection.First().Serialize();
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

                    File.WriteAllText(options.Output, amlRoot.ToString());
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
                var targetDir = Directory.GetParent(options.File).FullName;

                var outputFile = Path.Combine(targetDir, fileName + ".aml");
                if (!ShouldOverride(outputFile))
                    return;

                File.WriteAllText(outputFile, amlRoot.ToString());
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(AmlRiderLogo.Replace("@", Environment.NewLine));
            Console.WriteLine("For help use the --help flag!" + Environment.NewLine);

            new CommandLineParser()
                .Parse(args, typeof(ConvertOptions))
                .WithParsed<ConvertOptions>(OnConvertOptionsParsed);
        }

    }

}