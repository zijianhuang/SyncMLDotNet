using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;

namespace Fonlow.SyncML.Common
{
    /// <summary>
    /// Provide common helper Xml functions
    /// </summary>
    public sealed class XmlHelpers
    {
        private XmlHelpers()
        {

        }
        /// <summary>
        /// Safely query multiple levels of Xml path. If the path could not return any element,
        /// return an empty list rather than null. This is convenient for Linq to Xml.
        /// </summary>
        /// <param name="topElement"></param>
        /// <param name="elementNames"></param>
        /// <returns>Elements or empty list.</returns>
        public  static IEnumerable<XElement> SafeElementsQuery(XElement topElement, params string[] elementNames)
        {
            if (elementNames == null)
            {
                Trace.TraceWarning("Why are you passing no elementNames?");
                return new List<XElement>();
            }

            if (topElement == null)
            {
                Trace.TraceWarning("Why are you passing no topElement?.");
                return new List<XElement>();
            }

            IEnumerable<XElement> firstLevelElements = topElement.Elements(elementNames[0]);
            IEnumerable<XElement> r = firstLevelElements;
            if (firstLevelElements != null)
            {
                for (int i = 1; i < elementNames.Length; i++)
                {
                    IEnumerable<XElement> thisLevelElements = r.Elements(elementNames[i]);
                    if (thisLevelElements != null)
                    {
                        r = thisLevelElements;
                    }
                    else
                        return new List<XElement>();
                }

            }
            else
                return new List<XElement>();

            return r;
        }

    }
}
