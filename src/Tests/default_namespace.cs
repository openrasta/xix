using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
  public class default_namespace
  {
    [Test]
    public void can_use_default_namespace()
    {
      dynamic soap = new Xix("http://soap.org");
      Assert.That(soap.Envelope.ToString(), Is.EqualTo("<Envelope xmlns=\"http://soap.org\" />"));
    }

    [Test]
    public void can_use_child_elements_with_default_namespaces()
    {
      dynamic soap = new Xix("http://soap.org");
      Assert.That(soap.Envelope[soap.Body].ToString(),
        Is.EqualTo("<Envelope xmlns=\"http://soap.org\"><Body /></Envelope>"));
    }

    [Test]
    public void can_use_default_namespace_in_child()
    {
      dynamic xml = new Xix();
      dynamic soap = new Xix("http://soap.org");

      var d = xml.Example[soap.Body];
      Assert.That(d.ToString(), Is.EqualTo("<Example><Body xmlns=\"http://soap.org\" /></Example>"));
    }
  }
}