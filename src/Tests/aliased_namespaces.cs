﻿using NUnit.Framework;
using OpenRasta.Xix;

namespace Tests
{
  public class aliased_namespaces
  {
    [Test]
    public void can_use_namespace()
    {
      dynamic soap = new Xix("soap", "http://soap.org");
      Assert.That(soap.Envelope.ToString(), Is.EqualTo("<soap:Envelope xmlns:soap=\"http://soap.org\" />"));
    }

    [Test]
    public void namespace_declaration_once_only()
    {
      dynamic soap = new Xix("soap", "http://soap.org");
      Assert.That(soap.Envelope[soap.Body].ToString(),
        Is.EqualTo("<soap:Envelope xmlns:soap=\"http://soap.org\"><soap:Body /></soap:Envelope>"));
    }
  }
}