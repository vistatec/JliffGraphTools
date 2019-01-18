# JLIFF 2.0 Object Model
The JLIFF 2.0 Object Model contains classes and methods for creating and manipulating object graphs which closely reflect the OASIS JLIFF 2.0 serialization format. Object graphs can be serialized to schema conformant JSON. Conformant JSON can also be deserialized to an object graph. Convenience classes and methods are also provided to create a JLIFF object model (and thus a JSON serialization) from an XLIFF 2.0 Core XML file.

## Goals for this project
This project was originally concieved to validate the designs and ideas of the [OASIS OMOS Technical Committee](https://www.oasis-open.org/committees/tc_home.php?wg_abbrev=xliff-omos) but it is also hoped that it will provide a kind of reference implementation and foundation library for those wishing to evaluate JLIFF for their use cases, including, it is hoped, the GALA Localization Web Services Project.

Technical Committee outputs such as schema definitions and example serializations can be found on [GitHub](https://github.com/oasis-tcs/xliff-omos-jliff).

## Project status
As with the OASIS project itself, this project is a work in progress. I have a short attention span so some features may not be completely implmented because I got distracted by another feature during implementation.

Overall features should be complete with respect to XLIFF 2.0 Core. Implementation of module related features have to date been motivated by my own use cases.

## Latest ##
As of 2019-01-18 it is possible to convert XLIFF 2.1 Core to JLIFF 2.1 Core and JLIFF 2.1 Core back to XLIFF 2.1 Core. Yes, that's pretty simple to do with a generic JSON to XML converter or XSLT but with the library it is done via the Object Model which you can interact with and manipulate!

Added Builder Pattern graph construction:


This is only good for relatively simple graphs though.

Added Composite and Visitor Patterns so the whole graph can be traversed by creating a class that implements `ICompositeVisitor` and then calling `Process()` on the `JliffDocument.Files` property. This is how the `Segments` property is implemented.

## Object Model Creation Modes ##

### Fluent ###

    JliffDocument jd = new JliffDocument("en-US", "it-IT",
        new File("f1",
            new Unit("u1",
                new Segment(
                    new List<IElement()
                        { new SmElement(), new TextElement("Source text"), new EmElement() }),
                    new List<IElement()
                        { new SmElement(), new TextElement("Target text"), new EmElement() })
    ));

### Builder Pattern ###

*This is still experimental.*

    JliffDocument jlb = new JliffDocument("en-US", "it-IT")
        .AddFile()
        .AddUnit()
        .EndSubFiles()
        .AddSegment(new XlfEventArgs("s1"))
        .EndSubUnits()
        .AddSource("Hello")
        .AddSmElement("mrk1", true)
        .AddSource("there")
        .AddEmElement("", true)
        .AddTarget("Buongiorno")
        .MoreSubUnits()
        .AddSegment(new XlfEventArgs("s2"))
        .EndSubUnits()
        .AddSource("Random text")
        .MoreSubUnits()
        .AddSegment(new XlfEventArgs("s3"))
        .EndSubUnits()
        .AddSource("Goodbye")
        .AddTarget("Arrivederci")
        .Build();

### Event Driven ###

*Useful for building Extract Agents*

    MyContentType.NewParagraphEvent += JlBuilder.Paragraph;

## Branches ##
As of 2018-11-02 I have added a `dev` branch as a way of sharing bits of progress which have not been fully tested.

## Facilities

In addition to above I've also added:

* Composition and Visitor Patterns.

## Licensing
The XLIFF 2.0 Object Model is licensed under the MIT License.
