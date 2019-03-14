using System;
using System.Xml.Linq;
using AMLRider.Library.Rules;
/* Braucht es die Dinger nicht? Weird...
using AMLRider.Library.Rules.DatatypeRefRule;
using AMLRider.Library.Rules.DocumentInfoRule;
using AMLRider.Library.Rules.EventCollectionRule;
using AMLRider.Library.Rules.IConversionRule;
using AMLRider.Library.Rules.ProfileHeaderRule;
using AMLRider.Library.Rules.StdVariableRefRule;
using AMLRider.Library.Rules.VendorLogoRule;
*/

namespace AMLRider.Library
{
    public class RuleSelector
    {
        public static IConversionRule SelectRule(XElement element)
        {
            IConversionRule correctRule = null;
            var counter = 0;
            if (new DatatypeRefRule().CanApplyRule(element))
            {
                correctRule = new DatatypeRefRule();
                counter++;
            }
            if (new DocumentInfoRule().CanApplyRule(element))
            {
                correctRule = new DocumentInfoRule();
                counter++;
            }
            if (new EventCollectionRule().CanApplyRule(element))
            {
                correctRule = new EventCollectionRule();
                counter++;
            }
            if (new ProfileHeaderRule().CanApplyRule(element))
            {
                correctRule = new ProfileHeaderRule();
                counter++;
            }
            if (new StdVariableRefRule().CanApplyRule(element))
            {
                correctRule = new StdVariableRefRule();
                counter++;
            }
            if (new VendorLogoRule().CanApplyRule(element))
            {
                correctRule = new VendorLogoRule();
                counter++;
            }

            if (counter == 1)
            {
                return correctRule;
            }
            else
            {
                throw new InvalidOperationException(counter + " rules found that apply to " + element.Name);
            }
        }
    }
}