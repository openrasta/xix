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
nuget install OpenRasta.Xix -pre
```

## Documentation by example

### Simple xml
```csharp
dynamic xml = new Xix();

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

### Default namespaces
```csharp
dynamic html = new Xml("http://www.w3.org/1999/xhtml");

var document = html.html[html.body]
```
```xml
<html xmlns="http://www.w3.org/1999/xhtml">
  <body />
</html>
```

### Aliasing namespaces
```csharp
dynamic soap = new Xml("soap", "http://www.w3.org/2001/12/soap-envelope");

var document = soap.Envelope[soap:Body];
```
```xml
<soap:Envelope xmlns:soap="http://www.w3.org/2001/12/soap-envelope">
  <soap:Body />
</soap:Envelope>
```

### Mix and match
```csharp
dynamic xml = new Xix();
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

### Attributes
```csharp
dynamic xml = new Xix();

var document = xml.html.base("http://example.org/base");
```
```xml
<html base="http://example.org/base" />
```

### Boolean attributes

Note that this is the correct serialisation for boolean attributes, although
browsers are a bit more flexible than that.
```csharp
dynamic html = new Xix();
var doco = html.input.type("checkbox").@checked();
```
```xml
<input type="checkbox" checked="checked" />
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

### Using linq

```chsarp
var sashimis = new[] { "Ikura", "Tobiko" };
dynamic xml = new Xix();
var doco = xml.menu[sashimis.Select(_ => xml.sashimi[_])];
```

```xml
<menu>
  <sashimi>Ikura</sashimi>
  <sashimi>Tobiko</sashimi>
</menu>
```

### Interop with `System.Xml.Linq`

Any element and attibute created by Xix converts implicitly to the Linq types,
so you can use them interchangeably.

```csharp
dynamic xml = new Xix();
var doco = new XDocument(xml.body.base("http://google.com")));
XElement titleElement = xml.title;
```
```xml
<body base="http://google.com" />
```

XAttributes and XElement can be added to XixElements easily.
```csharp
dynamic html = new Xix();
var doco = html.head
            [new XElement("title", "hello")]
            [html.base.attr(new XAttribute("src", "http://example.org"))];
```

```xml
<head>
  <title>hello</title>
  <base src="http://example.org" />
</head>
```

Same can be done using collection of XElements, using Linq.
```csharp
var sushis = new[] {"Tobiko", "Ikura"};
dynamic xml = new Xix();
var doco = xml.sushis[sushis.Select(sushiName => new XElement("sushi", sushiName))];
```

```xml
<sushis>
  <sushi>Ikura</sushi>
  <sushi>Tobiko</sushi>
</sushis>
```

Same applies to attributes.
```csharp
var
## Todo List

 - [x] Prefixed namespace attributes
 - [x] Dash in attribute names e.g. 'my-attrib="..."'
 - [x] Enumerables as children
 - [x] System.Xml.Linq conversions
 - [x] HTML boolean attributes (element.attribute())
 - [ ] Often-used namespaces
 - [ ] Query support



## Ideas not yet requested by anyone yet

 - [ ] Concatenation to node lists with + operator (maybe?)
 - [ ] Serialization to HTML5 (low priority)
 - [ ] XmlDocument / XmlNode interop
 - [ ] Version without any ties to System.Xml.Linq (or System.Xml?)
