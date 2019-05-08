using System;
using System.IO;
using Iodd2AmlConverter.Cli;
using Iodd2AmlConverter.Cli.Attributes;
using Iodd2AmlConverter.Cli.Extensions;
using Iodd2AmlConverter.Library;

namespace Iodd2AmlConverter
{
    [Verb("package", HelpText = "Creates an AMLX-package out of an IODD file and its corresponding files (e.g. an icon)")]
    public class PackageOptions
    {
        [Option('f', "file", IsRequired = true, HelpText = "The IODD file to convert.")]
        public string File { get; set; }
    }
    
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

        private static bool HasParsedArgs { get; set; }

        private static string ReadFile(string path)
        {
            string fileText;
            try
            {
                fileText = File.ReadAllText(path);
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Unable to read file. Maybe you are missing permissions or the file is opened in another software.");
                return null;
            }

            return fileText;
        }

        private static string ConstructOutputFilePath(string path)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var targetDir = Directory.GetParent(path).FullName;

            return Path.Combine(targetDir, fileName + ".aml");
        }

        private static void OnConvertOptionsParsed(ConvertOptions options)
        {
            HasParsedArgs = true;
            if (!File.Exists(options.File))
            {
                Console.WriteLine($"The file {options.File} does not exist.");
                return;
            }

            var fileText = ReadFile(options.File);
            string amlRoot;

            var outputFile = options.Output;
            if (string.IsNullOrWhiteSpace(outputFile))
                outputFile = ConstructOutputFilePath(options.File);

            try
            {
                amlRoot = ConversionHandler.Convert(fileText, outputFile);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred during conversion. The file has probably an invalid format.");
                return;
            }

            try
            {
                if (!ShouldOverride(outputFile))
                    return;

                File.WriteAllText(outputFile, amlRoot);
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Unable to write output file. Maybe you are missing permissions or the file is opened in another software");
                return;
            }
        }
        
        private static void OnPackageOptionsParsed(PackageOptions options)
        {
            HasParsedArgs = true;
            if (!File.Exists(options.File))
            {
                Console.WriteLine($"The file {options.File} does not exist.");
                return;
            }

            var fileText = ReadFile(options.File);
            string amlRoot;
            var outputFile = ConstructOutputFilePath(options.File);
            try
            {
                amlRoot = ConversionHandler.Convert(fileText, outputFile);
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred during conversion. The file has probably an invalid format.");
                return;
            }

            try
            {
                if (!ShouldOverride(outputFile))
                    return;

                File.WriteAllText(outputFile, amlRoot);
            }
            catch (IOException)
            {
                Console.WriteLine(
                    "Unable to write output file. Maybe you are missing permissions or the file is opened in another software");
                return;
            }

            var packageHandler = new PackageHandler();
            packageHandler.CreatePackage(options.File, outputFile);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(AmlRiderLogo.Replace("@", Environment.NewLine));
            Console.WriteLine("For help use the --help flag!" + Environment.NewLine);

            new CommandLineParser()
                .Parse(args, typeof(ConvertOptions), typeof(PackageOptions))
                .WithParsed<ConvertOptions>(OnConvertOptionsParsed)
                .WithParsed<PackageOptions>(OnPackageOptionsParsed);

            if (HasParsedArgs && args.Length > 0)
                return;
            
            var path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine($"The file {path} does not exist.");
                return;
            }

            var outputFile = ConstructOutputFilePath(path);
            if (!ShouldOverride(outputFile))
                return;

            var fileText = ReadFile(path);
            var outputXml = ConversionHandler.Convert(fileText, outputFile);

            File.WriteAllText(outputFile, outputXml);
        }

    }

}