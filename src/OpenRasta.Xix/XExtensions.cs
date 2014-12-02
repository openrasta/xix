using System.Linq;
using System.Xml.Linq;

namespace OpenRasta.Xix
{
    public static class XExtensions
    {
        public static void RemoveRedundantXmlNamespaces(this XElement element)
        {
            var xmlnsDeclarations = element
                .Attributes()
                .Where(_ => _.IsNamespaceDeclaration &&
                            _.Name.Namespace == XNamespace.Xmlns)
                .Select(_ => new { Attribute = _, Prefix = _.Name.LocalName, Namespace = _.Value })
                .ToList();
            foreach (var ns in from ns in xmlnsDeclarations
                let parentNamespace = element.Parent.GetNamespaceOfPrefix(ns.Prefix)
                where parentNamespace == ns.Namespace
                select ns)
            {
                ns.Attribute.Remove();
            }
        }

        public static XElement XmlNs(this XElement element, string nsPrefix, XNamespace xNamespace)
        {
            element.Add(new XAttribute(XNamespace.Xmlns + nsPrefix, xNamespace));
            return element;
        }
    }
}