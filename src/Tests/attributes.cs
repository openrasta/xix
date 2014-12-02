using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
    class attributes
    {
        [Test]
        public void underscore_into_dash()
        {
            dynamic xml = new Xix();
            Assert.That(xml.root.attribute_name("value").ToString(),
              Is.EqualTo("<root attribute-name=\"value\" />"));
        }
        [Test]
        public void two_underscores_into_underscore()
        {
            dynamic xml = new Xix();
            Assert.That(xml.root.attribute__name("value").ToString(),
              Is.EqualTo("<root attribute_name=\"value\" />"));
        }
        [Test]
        public void three_underscores_and_youre_on_your_own()
        {
            dynamic xml = new Xix();
            Assert.That(xml.root.attribute___name("value").ToString(),
              Is.EqualTo("<root attribute_-name=\"value\" />"));
        }
        [Test]
        public void method_name_with_one_param()
        {
            dynamic _ = new Xix();
            var doco = _.rootElement.name("value");
            Assert.That(doco.ToString(), Is.EqualTo("<rootElement name=\"value\" />"));
        }

        [Test]
        public void custom_attribute_names()
        {
            dynamic xml = new Xix();
            var doco = xml.root.attr("name", "value");
            Assert.That(doco.ToString(), Is.EqualTo("<root name=\"value\" />"));
        }
    }
    public class namespaced_attributes
    {
        [Test]
        public void add_xmlns_on_first_occurence()
        {
            dynamic xml = new Xix();
            dynamic xlink = new Xix("xlink", "http://www.w3.org/1999/xlink");
            var attrib = xlink.href("http://google.com");
            var doco = xml.html.attr(attrib);
            Assert.That(doco.ToString(), Is.EqualTo("<html xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:href=\"http://google.com\" />"));
        }

        [Test]
        public void doesnt_add_on_second_occurence()
        {
            dynamic xml = new Xix();
            dynamic xlink = new Xix("xlink", "http://www.w3.org/1999/xlink");
            var doco = xml.html.attr(xlink.href("http://google.com"))[xml.body.attr(xlink.@base("http://bing.com"))];
            Assert.That(doco.ToString(), Is.EqualTo("<html xmlns:xlink=\"http://www.w3.org/1999/xlink\" xlink:href=\"http://google.com\">" +
                                                    "<body xlink:base=\"http://bing.com\" />" +
                                                    "</html>"));
        }
    }
}