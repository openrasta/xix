using System.Xml.Serialization;
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

        [Test]
        public void boolean_attributes()
        {
            dynamic html = new Xix();
            var doco = html.input.type("checkbox").@checked();
            Assert.That(doco.ToString(), Is.EqualTo("<input type=\"checkbox\" checked=\"checked\" />"));
        }
    }
}