using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
  internal class root_element
  {
    [Test]
    public void serialize_root()
    {
      dynamic _ = new Xix();
      var doco = _.rootElement;
      Assert.That(doco.ToString(), Is.EqualTo("<rootElement />"));
    }

  }
}