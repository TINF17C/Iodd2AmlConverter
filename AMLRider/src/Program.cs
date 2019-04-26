﻿using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using AMLRider.Cli;
using AMLRider.Cli.Attributes;
using AMLRider.Cli.Extensions;
using AMLRider.Library;
using AMLRider.Library.Iodd.Elements;
using NAudio.Wave;

namespace AMLRider
{
    [Verb("convert", HelpText = "Converts an IODD file to an AML file.")]
    public class ConvertOptions
    {
        [Option('f', "file", IsRequired = true, HelpText = "The IODD file to convert.")]
        public string File { get; set; }

        [Option('o', "output", IsRequired = false, HelpText = "Specifies the output file path.")]
        public string Output { get; set; }

        [Option('d', "dude", IsRequired = false, HelpText = "Surprise.")]
        public bool Dude { get; set; }
    }

    public static class Program
    {
        public static readonly string AmlRiderLogo =
            @"  ___            _______ _     _           @" +
            @" / _ \          | | ___ (_)   | |          @" +
            @"/ /_\ \_ __ ___ | | |_/ /_  __| | ___ _ __ @" +
            @"|  _  | '_ ` _ \| |    /| |/ _` |/ _ \ '__|@" +
            @"| | | | | | | | | | |\ \| | (_| |  __/ |   @" +
            @"\_| |_/_| |_| |_|_\_| \_|_|\__,_|\___|_|   @";
        
        private static Thread AvengersThread { get; set; }

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
            if (options.Dude)
                RunAvengersThemeAudioThread();

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

            // var handler = new ConversionHandler();
            // string convertedXml;


            try
            {
                // convertedXml = handler.Convert(fileText);
                var root = XElement.Parse(fileText);

                var device = new IODevice();
                device.Deserialize(root);

                int dummy;
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

                    // File.WriteAllText(options.Output, convertedXml);
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

                // File.WriteAllText(outputFile, convertedXml);
            }
        }

        private static void RunAvengersThemeAudioThread()
        {
            void Run()
            {
                var assembly = Assembly.GetExecutingAssembly();
                const string resourceName = "AMLRider.Resources.AvengersTheme.mp3";

                var stream = assembly.GetManifestResourceStream(resourceName);

                var mp3FileReader = new Mp3FileReader(stream);
                var waveStream = WaveFormatConversionStream.CreatePcmStream(mp3FileReader);
                var blockAlignReductionStream = new BlockAlignReductionStream(waveStream);

                var directOut = new DirectSoundOut(20);
                directOut.Init(blockAlignReductionStream);

                directOut.Play();
                while (directOut.PlaybackState == PlaybackState.Playing)
                    Thread.Sleep(100);
            }

            AvengersThread = new Thread(Run) {IsBackground = true};
            AvengersThread.Start();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(AmlRiderLogo.Replace("@", Environment.NewLine));

            new CommandLineParser()
                .Parse(args, typeof(ConvertOptions))
                .WithParsed<ConvertOptions>(OnConvertOptionsParsed);

            AvengersThread.Join();
        }
    }
}