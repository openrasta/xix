using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
    public class conversions
    {
        [Test]
        public void XixElement_to_XElement()
        {
            dynamic xml = new Xix();
            XElement doco = xml.root;
            Assert.That(doco.ToString(SaveOptions.DisableFormatting), Is.EqualTo("<root />"));
        }

        [Test]
        public void XixElement_to_XDocument()
        {
            dynamic xml = new Xix();
            XDocument doco = xml.root;
            Assert.That(doco.ToString(SaveOptions.DisableFormatting), Is.EqualTo("<root />"));
        }

        [Test]
        public void no_namespace_xix_attrib_to_xattribute()
        {
            dynamic xml = new Xix();
            XAttribute attrib = xml.attribName("value");
            Assert.That(attrib.Name.LocalName, Is.EqualTo("attribName"));
            Assert.That(attrib.Name.Namespace, Is.EqualTo(XNamespace.None));
        }

        [Test]
        public void namespaced_xix_attrib_to_xattribute()
        {
            dynamic xml = new Xix("http://www.w3.org/1999/xlink");
            XAttribute attrib = xml.attribName("value");
            Assert.That(attrib.Name.LocalName, Is.EqualTo("attribName"));
            Assert.That(attrib.Name.Namespace.ToString(), Is.EqualTo("http://www.w3.org/1999/xlink"));
        }

        [Test]
        public void add_XixAttribute_to_XElement()
        {
            dynamic xml = new Xix();
            var element = new XElement("root", xml.@base("http://google.com"));
            Assert.That(element.ToString(SaveOptions.DisableFormatting), Is.EqualTo("<root base=\"http://google.com\" />"));
        }

        [Test]
        public void add_XAttribute_to_XixElement()
        {
            dynamic xml = new Xix();
            var element = xml.html.attr(new XAttribute("base", "http://bing.com"));
            Assert.That(element.ToString(), Is.EqualTo("<html base=\"http://bing.com\" />"));
        }
        [Test]
        public void add_XixElement_to_XDocument()
        {
            dynamic xml = new Xix();
            var doco = new XDocument(xml.root);
            Assert.That(doco.ToString(SaveOptions.DisableFormatting), Is.EqualTo("<root />"));
        }

        [Test]
        public void add_XElement_to_XixElement()
        {
            dynamic xml = new Xix();
            var doco = new XDocument(xml.root[new XElement("body")]);
            Assert.That(doco.ToString(SaveOptions.DisableFormatting), Is.EqualTo("<root><body /></root>"));
        }

        [Test]
        public void add_enumerable_of_XElements()
        {
            var sushis = new[] {"Tobiko", "Ikura"};
            dynamic xml = new Xix();
            var doco = xml.sushis[sushis.Select(sushiName => new XElement("sushi", sushiName))];
            Assert.That(doco.ToString(),
                Is.EqualTo("<sushis><sushi>Tobiko</sushi><sushi>Ikura</sushi></sushis>"));
        }
        [Test]
        public void add_enumerable_of_XAttributes()
        {
            var sushis = new[] { "Tobiko", "Ikura" };
            dynamic xml = new Xix();
            var doco = xml.sushis.attr(sushis.Select(sushiName => new XAttribute(sushiName, "yes")));
            Assert.That(doco.ToString(),
                Is.EqualTo("<sushis Tobiko=\"yes\" Ikura=\"yes\" />"));
        }
    }
}
