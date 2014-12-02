using System.Xml.Linq;

namespace OpenRasta.Xix
{
    public class XixAttribute
    {
        private readonly XAttribute _wrappedAttribute;
        private readonly XNamespace _xNamespace;
        private readonly string _nsPrefix;

        public XixAttribute(string nsPrefix, XNamespace xNamespace, string name, object value)
        {
            _nsPrefix = nsPrefix;
            _xNamespace = xNamespace;
            _wrappedAttribute = new XAttribute(xNamespace + name, value);
        }

        public void AttachTo(XElement element)
        {
            element.XmlNs(_nsPrefix, _xNamespace).Add(_wrappedAttribute);
           
        }
    }
}