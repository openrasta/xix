using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace OpenRasta.Xix
{
  public class DynamicElement : DynamicObject
  {
    public XElement WrappedElement;

    public DynamicElement(XNamespace xNamespace, string name)
    {
      WrappedElement = new XElement(xNamespace + name);
      Namespace = xNamespace;
    }

    public DynamicElement(string nsPrefix, XNamespace xNamespace, string name)
    {
      NamespacePrefix = nsPrefix;
      Namespace = xNamespace;
      WrappedElement = new XElement(xNamespace + name, new XAttribute(XNamespace.Xmlns + nsPrefix, xNamespace));
    }

    public XNamespace Namespace { get; set; }

    public string NamespacePrefix { get; set; }

    public DynamicElement this[DynamicElement childElement]
    {
      get
      {
        var element = childElement.WrappedElement;

        WrappedElement.Add(element);
        var xmlnsDeclarations = element
          .Attributes()
          .Where(_ => _.IsNamespaceDeclaration &&
                      _.Name.Namespace == XNamespace.Xmlns)
          .Select(_ => new {Attribute = _, Prefix = _.Name.LocalName, Namespace = _.Value})
          .ToList();
        foreach (var ns in from ns in xmlnsDeclarations
                           let parentNamespace = WrappedElement.GetNamespaceOfPrefix(ns.Prefix)
                           where parentNamespace == ns.Namespace
                           select ns)
        {
          ns.Attribute.Remove();
        }
        return this;
      }
    }

    public DynamicElement this[string textNode]
    {
      get
      {
        WrappedElement.Add(new XText(textNode));
        return this;
      }
    }

    public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    {
      var newProperty = new XAttribute(binder.Name.ConvertUndescores(), args[0]);
      WrappedElement.Add(newProperty);
      result = this;
      return true;
    }

    public override string ToString()
    {
      return WrappedElement.ToString(SaveOptions.DisableFormatting);
    }
  }
}