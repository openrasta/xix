using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;
using OpenRasta.Xix.Annotations;

namespace OpenRasta.Xix
{
    public class XixElement : DynamicObject
    {
        private readonly XElement _wrappedElement;

        public XixElement(XNamespace @namespace, string localName)
        {
            _wrappedElement = new XElement(@namespace + localName);
        }

        public XixElement(string namespacePrefix, XNamespace @namespace, string localName)
        {
            _wrappedElement = new XElement(@namespace + localName).XmlNs(namespacePrefix, @namespace);
        }

        [UsedImplicitly]
        public XixElement this[XElement childElement]
        {
            get
            {
                _wrappedElement.Add(childElement);
                return this;
            }
        }

        [UsedImplicitly]
        public XixElement this[IEnumerable<XElement> childElements]
        {
            get
            {
                _wrappedElement.Add(childElements.Cast<object>().ToArray());
                return this;
            }
        }
        [UsedImplicitly]
        public XixElement this[IEnumerable<dynamic> childElements]
        {
            get
            {
                foreach (var childElement in childElements) AddChildNode(childElement);
                return this;
            }
        }

        [UsedImplicitly]
        public XixElement this[XixElement childElement]
        {
            get
            {
                childElement.AttachTo(_wrappedElement);
                return this;
            }
        }


        [UsedImplicitly]
        public XixElement this[string textNode]
        {
            get
            {
                _wrappedElement.Add(new XText(textNode));
                return this;
            }
        }

        private void AddChildNode(dynamic childElement)
        {
            childElement.AttachTo(_wrappedElement);
        }

        private void AttachTo(XElement parentElement)
        {
            parentElement.Add(_wrappedElement);

            _wrappedElement.RemoveRedundantXmlNamespaces();
        }

        // ReSharper disable once InconsistentNaming - on purpose
        [UsedImplicitly]
        public XixElement attr(XixAttribute attribute)
        {
            attribute.AttachTo(_wrappedElement);
            return this;
        }

        // ReSharper disable once InconsistentNaming - on purpose
        [UsedImplicitly]
        public XixElement attr(XAttribute attribute)
        {
            _wrappedElement.Add(attribute);
            return this;
        }
        // ReSharper disable once InconsistentNaming - on purpose
        [UsedImplicitly]
        public XixElement attr(IEnumerable<XAttribute> attributes)
        {
            _wrappedElement.Add(attributes.Cast<object>().ToArray());
            return this;
        }

        // ReSharper disable once InconsistentNaming - on purpose
        [UsedImplicitly]
        public XixElement attr(string name, string value)
        {
            _wrappedElement.Add(new XAttribute(name, value));
            return this;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (args.Length == 0)
            {
                var attribName = binder.Name.ConvertUndescores();
                return AddNameValueAttribute(attribName, attribName, out result);
            }
            if (args.Length == 1)
            {
                return AddNameValueAttribute(binder.Name.ConvertUndescores(), args[0], out result);
            }

            result = null;
            return false;
        }

        private bool AddNameValueAttribute(string name, object value, out object result)
        {
            _wrappedElement.Add(new XAttribute(name, value));
            result = this;
            return true;
        }

        public override string ToString()
        {
            return _wrappedElement.ToString(SaveOptions.DisableFormatting);
        }

        public static implicit operator XElement(XixElement xix)
        {
            return xix._wrappedElement;
        }

        public static implicit operator XDocument(XixElement xix)
        {
            return new XDocument(xix._wrappedElement);
        }

    }
}