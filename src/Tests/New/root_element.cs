using NUnit.Framework;
using OpenRasta.Xix;

namespace ConsoleApplication1.New
{
  internal class root_element
  {
    [Test]
    public void serialize_root()
    {
      dynamic _ = new Xml();
      var doco = _.rootElement;
      Assert.That(doco.ToString(), Is.EqualTo("<rootElement />"));
    }

    [Test]
    public void serialize_attribs()
    {
      dynamic _ = new Xml();
      var doco = _.rootElement.name("value");
      Assert.That(doco.ToString(), Is.EqualTo("<rootElement name=\"value\" />"));
    }
  }
}