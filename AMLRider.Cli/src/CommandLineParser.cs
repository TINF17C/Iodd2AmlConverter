using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public CommandLineParser()
        {
        }

        public ParserResult Parse(IEnumerable<string> arguments, params Type[] types)
        {
            if (arguments == null || !arguments.Any())
            {
                // TODO: Generate help.
                throw new ArgumentNullException(nameof(arguments));
            }

            if(types == null)
                throw new ArgumentNullException(nameof(types));

            var verbs = Verb.SelectFromTypes(types);

            var firstArg = arguments.First();
            var type = verbs
                .Where(v => v.Key.Name == firstArg)
                .Select(v => v.Value)
                .FirstOrDefault();

            if (type == null)
            {
                // TODO: Generate help.
                return new NotParsedResult();
            }

            // TODO: Preprocess arguments for help (and maybe version).
            
            return ParseOptions(arguments.Skip(1), type);
        }

        private ParserResult ParseOptions(IEnumerable<string> arguments, Type targetType)
        {
            var optionProperties = targetType.GetOptionProperties();
            if(optionProperties == null || !optionProperties.Any())
                throw new InvalidOperationException($"The specified type {targetType.Name} does not have any properties marked with {nameof(OptionAttribute)}.");

            var tokens = Tokenizer.Tokenize(arguments);
            if (tokens == null || !tokens.Any())
                return new NotParsedResult();

            var pairs = TokenPartitioner.Partition(tokens);

            // TODO: Check required options.
            
            var newObj = Activator.CreateInstance(targetType);
            OptionMapper.Map(optionProperties, pairs, newObj);

            return new ParsedResult(newObj);
        }

    }
    
}