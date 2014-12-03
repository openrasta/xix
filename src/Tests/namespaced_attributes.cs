using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
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