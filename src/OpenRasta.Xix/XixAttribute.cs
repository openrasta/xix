using System.Xml.Linq;

namespace OpenRasta.Xix
{
    public class XixAttribute : XAttribute
    {
        private readonly string _nsPrefix;
        private readonly XNamespace _xNamespace;

        public XixAttribute(string nsPrefix, XNamespace xNamespace, string name, object value)
            : base(xNamespace + name, value)
        {
            _nsPrefix = nsPrefix;
            _xNamespace = xNamespace;
        }

        public void AttachTo(XElement element)
        {
            element.XmlNs(_nsPrefix, _xNamespace).Add(this);
        }
    }
}