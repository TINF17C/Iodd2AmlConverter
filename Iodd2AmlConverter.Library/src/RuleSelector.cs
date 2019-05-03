using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Iodd2AmlConverter.Library.Rules;

namespace Iodd2AmlConverter.Library
{
    
    /// <summary>
    /// Responsible for selecting the correct rule.
    /// </summary>
    public class RuleSelector
    {

        /// <summary>
        /// Holds the available rules.
        /// </summary>
        private List<IConversionRule> Rules { get; }
        
        private Dictionary<string, IConversionRule> RuleCache { get; }
        
        /// <summary>
        /// Creates a new <see cref="RuleSelector"/> instance.
        /// </summary>
        public RuleSelector()
        {
            Rules = new List<IConversionRule>(GetAvailableRules());
            RuleCache = new Dictionary<string, IConversionRule>();
        }

        /// <summary>
        /// Gets all available rules.
        /// </summary>
        /// <returns>An enumerable of conversion rules.</returns>
        private static IEnumerable<IConversionRule> GetAvailableRules()
        {
            yield return new DatatypeRefRule();
            yield return new DeviceIdentityRule();
            yield return new DeviceVariantCollectionRule();
            yield return new DocumentInfoRule();
            yield return new EventCollectionRule();
            yield return new FeaturesRule();
            yield return new IoDeviceRule();
            yield return new ProcessDataCollectionRule();
            yield return new ProfileBodyRule();
            yield return new ProfileHeaderRule();
            yield return new StdVariableRefRule();
            yield return new VariableRecordTRule();
            yield return new VariableVSnRwRule();
            yield return new VendorLogoRule();
        }
        
        /// <summary>
        /// Selects the correct rule from a given XML element.
        /// </summary>
        /// <param name="element">The element to find a rule for.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IConversionRule SelectRule(XElement element)
        {
            var elementName = element.Name.ToString();
            if (RuleCache.ContainsKey(elementName))
                return RuleCache[elementName];
            
            var fittingRules = new List<IConversionRule>();
            foreach (var rule in Rules)
            {
                if(rule.CanApplyRule(element))
                    fittingRules.Add(rule);
            }
            
            if(fittingRules.Count > 1)
                throw new InvalidOperationException($"There are {fittingRules.Count} rules available for the element: {element}");

            if (fittingRules.Count == 0)
                return null;
            
            var selectedRule = fittingRules.First();
            RuleCache[elementName] = selectedRule;

            return selectedRule;
        }
        
    }
    
}