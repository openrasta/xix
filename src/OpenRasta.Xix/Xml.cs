using System.Dynamic;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OpenRasta.Xix
{
  public class Xml : DynamicObject
  {
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
      var name = binder.Name.ConvertUndescores();

      result = _nsPrefix != null
        ? new DynamicElement(_nsPrefix, _xNamespace, name)
        : new DynamicElement(_xNamespace, name);


      return true;
    }
  }
}