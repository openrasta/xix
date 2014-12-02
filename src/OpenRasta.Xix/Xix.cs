using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OpenRasta.Xix
{
    public class Xix : DynamicObject
    {
        private readonly string _nsPrefix;
        private readonly XNamespace _xNamespace;

        public Xix(string nsExpanded = "")
        {
            _xNamespace = XNamespace.Get(nsExpanded);
        }

        public Xix(string nsPrefix, string nsExtended)
        {
            _xNamespace = XNamespace.Get(nsExtended);
            _nsPrefix = nsPrefix;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (args.Length == 1)
            {
                result = new XixAttribute(_nsPrefix, _xNamespace, binder.Name.ConvertUndescores(), args[0]);
                return true;
            }
            return base.TryInvokeMember(binder, args, out result);
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name.ConvertUndescores();

            result = _nsPrefix != null
              ? new XixElement(_nsPrefix, _xNamespace, name)
              : new XixElement(_xNamespace, name);


            return true;
        }
    }
}