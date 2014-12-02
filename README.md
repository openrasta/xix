[![Build status](https://ci.appveyor.com/api/projects/status/h30pjv455g6wp79e?svg=true)](https://ci.appveyor.com/project/serialseb/xix)


# OpenRasta Xix Library

Tired of noisy XElements to generate your xml? Xix (standing for Xix Is Xml) is
the smallest possible library that allows writing xml simply and succinctly.


##Installing

Latest builds of Xix are available on the AppVeyor project feed, at `https://ci.appveyor.com/nuget/xix-k703x7hdb0fb`

To add a source, from the command line, use `nuget sources`.

`C:\> nuget sources add -name xix -source https://ci.appveyor.com/nuget/xix-k703x7hdb0fb`

You can then install from the package manager console.

```
install-package OpenRasta.Xix -pre
```

Or from any shell window, use `nuget.exe`.

```
nuget install xix -pre
```

## Documentation by example

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

    svg.width-height
```
```xml
<codeSample>
  <html xmlns="http://www.w3.org/1999/xhtml">
    <svg:svg width="200px" height="100px" />
  </html>
</codeSample>
```

### namespaced-attributes
In XML, the namespace of an attribute is dependent on the interpretation of the element that contains them. To enforce a namespace for an attribute, one needs to use the prefixed syntax.

```csharp
dynamic xml = new Xix();
dynamic xlink = new Xix("xlink", "http://www.w3.org/1999/xlink");
var doco = xml.html.attr(xlink.href("http://google.com"))
			[xml.body.attr(xlink.@base("http://bing.com"))];
```

```xml
<html xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="http://google.com">
	<body xlink:base="http://bing.com" />
</html>
```
## Todo List

 - [ ] Serialization to HTML5 (low priority)
 - [x] Prefixed namespace attributes
 - [x] Dash in attribute names e.g. 'my-attrib="..."'
 - [ ] XML prolog & Doctype
 - [ ] Enumerables as children
 - [ ] Concatenation to node lists with + operator
