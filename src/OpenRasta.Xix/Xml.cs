using System.Dynamic;
using System.Xml.Linq;

namespace OpenRasta.Xix
{
  public class Xml : DynamicObject
  {
    private readonly string _nsExpanded;
    private readonly string _nsPrefix;
    private readonly XNamespace _xNamespace;

    public Xml(string nsExpanded = "")
    {
      _xNamespace = XNamespace.Get(nsExpanded);
    }

    public Xml(string nsPrefix, string nsExtended)
    {
      _xNamespace = XNamespace.Get(nsExtended);
      _nsPrefix = nsPrefix;
    }


    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
      result = _nsPrefix != null
        ? new DynamicElement(_nsPrefix, _xNamespace, binder.Name)
        : new DynamicElement(_xNamespace, binder.Name);


      return true;
    }
  }
}