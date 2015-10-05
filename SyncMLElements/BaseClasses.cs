using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Fonlow.SyncML.Elements
{

    /// <summary>
    /// Basic type of all elements
    /// </summary>
    public abstract class SyncMLElement
    {
        protected XElement Element { get; set; }
   /*     public SyncMLElement()
        {
            Element = new XElement("d");//dummy name needed to avoid error
        }*/

        /// <summary>
        /// XML of whole element. Each element class will produce XmlText from memebers.
        /// </summary>
        public abstract XElement Xml { get; }

    /*    public void Rename(XName name)
        {
            Element.Name = name;
        }

        public void Rename(XNamespace ns, string localName)
        {
            Element.Name = ns + localName;
        }*/
    }

    /// <summary>
    /// For elements with text only
    /// </summary>
    public abstract class SyncMLSimpleElement : SyncMLElement
    {
      /*  internal SyncMLSimpleElement(XName elementName)
        {
            Element = new XElement(elementName);
        }*/

        internal SyncMLSimpleElement(string localName):this(ElementNames.SyncMLVersion, localName)
        {
        }

        internal SyncMLSimpleElement(XNamespace ns, string localName)
        {
            Element = new XElement(ns + localName);
        }
        /// <summary>
        /// Text contained.
        /// </summary>
        public string Content
        {
            get { return Element.Value; }
            set { Element.Value = value; }
        }

        public IEnumerable<XElement> Elements
        {
            get { return Element.Elements(); }
            set
            {
                if (value != null)
                {
                    Element.Add(value);
                }
            }
        }

        public override XElement Xml
        {
            get { return Element; }
        }

    }

    /// <summary>
    /// For elements with children elements and attributes.
    /// The derived classes use the following conventions to map the schema of SyncML schema to object models:
    /// 1. Xml element to property
    /// 2. For optional property, the Getter will always check whether the storage is null. If null, create one.
    /// 3. For optional property, XmlText will always check whether the property is null. If null, return empty string.
    /// 4. For property, the Setter will throw exception if assigned with null.
    /// 5. For multiple elements of the same name, use Collection (readonly property)
    /// 6. The default constructor is hidden, and 2 static Create functions will be implemented.
    /// </summary>
    public abstract class SyncMLComplexElement : SyncMLElement
    {
        protected SyncMLComplexElement(XName elementName)
        {
            Element = new XElement(elementName);
        }

        /// <summary>
        /// Create element with default SyncML1.2 namespace.
        /// </summary>
        /// <param name="elementName"></param>
        protected SyncMLComplexElement(string elementName):this(ElementNames.SyncMLVersion, elementName)
        {
        }

        protected SyncMLComplexElement(XNamespace ns, string localName)
        {
            Element = new XElement(ns + localName);
        }

        protected static void ThrowExceptionIfNull(SyncMLElement e, string elementName)
        {
            if (e == null)
                throw new ArgumentNullException(String.Format("Should not assign to this element {0} with null.", elementName));
        }
    }

    /// <summary>
    /// Parent class of SyncML commands, with common properties
    /// </summary>
    public abstract class SyncMLCommand : SyncMLComplexElement
    {
        protected SyncMLCommand(XName elementName)
            : base(elementName)
        {
        }

        protected SyncMLCommand(XNamespace sp, string localName)
            : base(sp, localName)
        {

        }

        protected SyncMLCommand(string localName)
            : this(ElementNames.SyncMLVersion, localName)
        {

        }

        private SyncMLCmdID cmdID = new SyncMLCmdID();

        public SyncMLCmdID CmdID
        {
            get { return cmdID; }
            set { cmdID = value; ThrowExceptionIfNull(value, "Add/CmdID"); }
        }

    }


    /// <summary>
    /// Helper class to read sub elements with the same namespace of the parent.
    /// </summary>
    internal class ElementReader
    {
        private  ElementReader()
        {

        }

        internal static XElement Element(XElement xmlData, string elementName)
        {
            return xmlData.Element(xmlData.Name.Namespace + elementName);
        }

        internal static IEnumerable<XElement> Elements(XElement xmlData, string elementName)
        {
            return xmlData.Elements(xmlData.Name.Namespace + elementName);
        }
    }

    /// <summary>
    /// Create a simple SyncML element for XML.
    /// </summary>
    public sealed class SyncMLSimpleElementFactory
    {
        private SyncMLSimpleElementFactory()
        {

        }

        public static T Create<T>(XElement xmlData) where T : SyncMLSimpleElement, new()
        {//CA1004 is not applicable here.
            if (xmlData == null)
                return null;

            T r = new T();
            if (xmlData.HasElements)
            { 
                r.Elements = xmlData.Elements(); 
            }
            else
            {
                r.Content = xmlData.Value;  // xmlData.Value will return text content of sub-elements
            }

            return r;
        }

        public static T Create<T>(string content) where T : SyncMLSimpleElement, new()
        {
            T r = new T();
            r.Content = content;
            return r;
        }

    }


}
