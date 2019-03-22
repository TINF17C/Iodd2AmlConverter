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
            var xElement = XElement.Parse(ioddFileData);

            var state = 0;

            var returnVal = "";


            foreach (var el in xElement.Elements())
            {
                if (state == 0)
                {
                    returnVal += RuleSelector.SelectRule(el).Apply(el).ToString();
                    state++;
                    continue;
                }

                if (state == 1)
                {
                    returnVal += RuleSelector.SelectRule(el).Apply(el).ToString();
                    state++;
                    continue;
                }

                if (state == 2)
                {
                    var builder = RuleSelector.SelectRule(el).Apply(el);

                    foreach (var current in el.Elements())
                    {
                        if (RuleSelector.SelectRule(current) != null)
                        {
                            builder.Add(RuleSelector.SelectRule(current).Apply(current));
                        }
                    }

                    returnVal += builder.ToString();
                    state++;
                    continue;
                }

                if (state == 3)
                {
                    returnVal += RuleSelector.SelectRule(el).Apply(el);
                }

        }

            return returnVal;
        }
    }
}