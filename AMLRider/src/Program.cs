using System;
using AMLRider.Cli;
using AMLRider.Cli.Attributes;
using AMLRider.Cli.Extensions;
using AMLRider.Cli.Helpers;

namespace AMLRider
{

    [Verb("convert")]
    public class ConvertOptions
    {
        
        [Option('f', "file", IsRequired = true, HelpText = "The IODD file to convert.")]
        public string File { get; set; }
        
        [Option('o', "output", IsRequired = false, HelpText = "Specifies the output file path.")]
        public string Output { get; set; }
        
    }
    
    public static class Program
    {

        public static void OnConvertOptionsParsed(ConvertOptions options)
        {
            Console.WriteLine(options.File);
            Console.WriteLine(options.Output);
            
            // TODO: Do conversion.
        }
        
        public static void Main(string[] args)
        {
            new CommandLineParser()
                .Parse(args, typeof(ConvertOptions))
                .WithParsed<ConvertOptions>(OnConvertOptionsParsed);
        }
        
    }
    
}