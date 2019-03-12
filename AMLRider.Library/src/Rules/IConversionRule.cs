using System.Xml;
using System.Xml.Linq;

namespace AMLRider.Library.Rules
{
    
    /// <summary>
    /// Defines a conversion rule.
    /// </summary>
    public interface IConversionRule
    {

        /// <summary>
        /// Checks whether this rule can be applied on the given element.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True, if the rule applies on this element.</returns>
        bool CanApplyRule(XElement element);
        
        /// <summary>
        /// Applies the conversion rule on the given <see cref="XElement"/>.
        /// </summary>
        /// <param name="element">The element to apply the rule on.</param>
        /// <returns>A new <see cref="XElement"/> with the converted content.</returns>
        XElement Apply(XElement element);

    }
    
}