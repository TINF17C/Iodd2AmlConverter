using System;
using System.Data;
using System.Xml.Linq;
using AMLRider.Library.Rules.DataObjects;

namespace AMLRider.Library
{
    /// <inheritdoc />
    /// <summary>
    /// Responsible for converting IODD files into AML files.
    /// </summary>
    public class ConversionHandler : IConversionHandler
    {
        /// <inheritdoc />
        /// <summary>
        /// Converts an IODD file into an AML file.
        /// </summary>
        /// <param name="ioddFileData">The content of an IODD file as a string.</param>
        /// <returns>Returns a string which contains the generated AML data.</returns>
        public string Convert(string ioddFileData)
        {
            var ruleSelector = new RuleSelector();
            
            var xElement = XElement.Parse(ioddFileData);

            var state = 0;

            var returnVal = "";


            foreach (var el in xElement.Elements())
            {
                if (state == 0)
                {
                    state++;
                    if (ruleSelector.SelectRule(el) == null)
                    {
                        continue;
                    }
                    returnVal += ruleSelector.SelectRule(el).Apply(el).ToString();
                    continue;
                }

                if (state == 1)
                {
                    state++;
                    if (ruleSelector.SelectRule(el) == null)
                    {
                        continue;
                    }
                    returnVal += ruleSelector.SelectRule(el).Apply(el).ToString();
                    continue;
                }

                if (state == 2)
                {
                    state++;
                    var builder = ruleSelector.SelectRule(el).Apply(el);

                    foreach (var current in el.Elements())
                    {
                        if (ruleSelector.SelectRule(current) != null)
                        {
                            builder.Add(ruleSelector.SelectRule(current).Apply(current));
                        }
                    }

                    returnVal += builder.ToString();
                    continue;
                }

                if (state == 3)
                {
                    returnVal += ruleSelector.SelectRule(el).Apply(el);
                }

        }

            return returnVal;
        }
    }
}