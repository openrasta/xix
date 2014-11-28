# OpenRasta Xix Library

Tired of noisy XElements to generate your xml? Xix (standing for Xix Is Xml) is
the smallest possible library that allows writing xml simply and succinctly.

##Installing

When Xix makes it way to nuget, installing it to a project is simple, as it has
no dependencies.

```
install-package OpenRasta.xix
```

## Examples

### Simple xml
```csharp
dynamic xml = new Xml();

var document = xml.html
                [xml.head[xml.title["Title"]]
                [xml.body["Some text"]]
```

```xml
<html>
  <head>
    <title>Title</title>
  </head>
  <body>
    Some tet
  </body>
</html>
```

### Attributes
```csharp
dynamic xml = new Xml();

var document = xml.html.base("http://example.org/base");
```
```xml
<html base="http://example.org/base" />
```

### Default namespaces
```chsarp
dynamic html = new Xml("http://www.w3.org/1999/xhtml");

var document = html.html[html.body]
```
```xml
<html xmlns="http://www.w3.org/1999/xhtml">
  <body />
</html>
```

### Aliasing namespaces
```chsharp
dynamic soap = new Xml("soap", "http://www.w3.org/2001/12/soap-envelope");

var document = soap.Envelope[soap:Body];
```
```xml
<soap:Envelope xmlns:soap="http://www.w3.org/2001/12/soap-envelope">
  <soap:Body />
</soap:Envelope>
```

### Mix and match
```chsarp
dynamic xml = new Xml();
dynamic html = new Xml("http://www.w3.org/1999/xhtml");
dynamic svg = new Xml("svg", "http://www.w3.org/2000/svg");

var document =
  xml.codeSample
    [html.head[html.body
      [svg.width("200px").height("100px")]
    ]];
```
```xml
<codeSample>
  <html xmlns="http://www.w3.org/1999/xhtml">
    <svg:svg width="200px" height="100px" />
  </html>
</codeSample>
```

## Todo List

 - [ ] Serialization to HTML5 (low priority)
 - [ ] Prefixed namespace attributes
 - [ ] Dash in attribute names e.g. 'my-attrib="..."'
 - [ ] XML prolog & Doctype
 - [ ] Enumerables as children
 - [ ] Concatenation to node lists with + operator
 
