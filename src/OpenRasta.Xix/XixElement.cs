using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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

        public XixElement this[IEnumerable<dynamic> childElements]
        {
            get
            {
                foreach (var childElement in childElements) childElement.AttachTo(_wrappedElement);
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

        private void AttachTo(XElement parentElement)
        {
            parentElement.Add(_wrappedElement);

            _wrappedElement.RemoveRedundantXmlNamespaces();
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

        // ReSharper disable once InconsistentNaming - on purpose
        public XixElement attr(XixAttribute attribute)
        {
            attribute.AttachTo(_wrappedElement);
            return this;
        }
        // ReSharper disable once InconsistentNaming - on purpose
        public XixElement attr(string name, string value)
        {
            _wrappedElement.Add(new XAttribute(name, value));
            return this;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (args.Length != 1)
            {
                result = null;
                return false;
            }

            _wrappedElement.Add(new XAttribute(binder.Name.ConvertUndescores(), args[0]));
            result = this;
            return true;
        }

        public override string ToString()
        {
            return _wrappedElement.ToString(SaveOptions.DisableFormatting);
        }
    }
}