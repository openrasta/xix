using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
  public class child_element
  {
    [Test]
    public void serialize_graph()
    {
      dynamic _ = new Xml();
      var doco = _.rootElement[_.childElement];
      Assert.That(doco.ToString(), Is.EqualTo("<rootElement><childElement /></rootElement>"));
    }

    [Test]
    public void serialize_graph_attribs()
    {
      dynamic _ = new Xml();
      var doco =
        _.rootElement[
          _.childElement.name("value")
          ];
      Assert.That(doco.ToString(), Is.EqualTo("<rootElement><childElement name=\"value\" /></rootElement>"));
    }
  }
}