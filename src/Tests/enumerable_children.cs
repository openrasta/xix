using System.Linq;
using System.Xml.Serialization;
using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
    public class enumerable_children
    {
        public void can_be_added()
        {
            var sashimis = new[] { "Ikura", "Tobiko" };
            dynamic xml = new Xix();
            var doco = xml.menu[sashimis.Select(_ => xml.sashimi[_])];
            Assert.That(doco.ToString(), Is.EqualTo("<menu><sashimi>Ikura</sashimi><sashimi>Tobiko</sashimi></menu>"));
        }
    }
}