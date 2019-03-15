using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AMLRider.Cli.Attributes;
using AMLRider.Cli.Core;
using AMLRider.Cli.Extensions;
using AMLRider.Cli.Result;

namespace AMLRider.Cli
{
    /// <summary>
    /// Only one verb can be used at a time!
    /// verb --option=value -o=value --bool_flag
    /// </summary>
    public class CommandLineParser
    {
        
        
        private HelpGenerator Help { get; }

        public CommandLineParser()
        {
            Help = new HelpGenerator();
        }

        public ParserResult Parse(IEnumerable<string> args, params Type[] types)
        {
            if (args == null || !args.Any())
                return new NotParsedResult();

            if (types == null)
                throw new ArgumentNullException(nameof(types));

            var type = GetVerbType(args, types);
            if (type != null)
                return ParseOptions(args.Skip(1), type);

            PrintHelpForTypes(types);
            return new NotParsedResult();
        }

        private ParserResult ParseOptions(IEnumerable<string> arguments, Type targetType)
        {
            var optionProperties = targetType.GetOptionProperties();
            if (optionProperties == null || !optionProperties.Any())
                throw new InvalidOperationException(
                    $"The specified type {targetType.Name} does not have any properties marked with {nameof(OptionAttribute)}.");

            var tokens = Tokenizer.Tokenize(arguments);
            if (tokens == null || !tokens.Any())
            {
                PrintHelpForTypes(new[] {targetType});
                return new NotParsedResult();
            }

            var pairs = TokenPartitioner.Partition(tokens);

            try
            {
                var newObj = Activator.CreateInstance(targetType);
                OptionMapper.Map(optionProperties, pairs, newObj);

                return new ParsedResult(newObj);
            }
            catch (InvalidOperationException)
            {
                PrintHelpForTypes(new[] {targetType});
            }

            return new NotParsedResult();
        }

        private static Type GetVerbType(IEnumerable<string> args, params Type[] types)
        {
            var verbs = Verb.SelectFromTypes(types);

            var firstArg = args.First();
            return verbs
                .Where(v => v.Key.Name == firstArg)
                .Select(v => v.Value)
                .FirstOrDefault();
        }
        
        private void PrintHelpForTypes(IEnumerable<Type> types)
        {
            var stringBuilder = new StringBuilder();
            foreach (var type in types)
            {
                var helpText = Help.Generate(type);
                stringBuilder.AppendLine(helpText);
            }

            var help = stringBuilder.ToString();
            Console.WriteLine(help);
        }
        
    }
}