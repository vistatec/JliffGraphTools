# JLIFF 2.0 Object Model
The JLIFF 2.0 Object Model contains classes and methods for creating and manipulating object graphs which closely reflect the OASIS JLIFF 2.0 serialization format. Object graphs can be serialized to schema conformant JSON. Conformant JSON can also be deserialized to an object graph. Convenience classes and methods are also provided to create a JLIFF object model (and thus a JSON serialization) from an XLIFF 2.0 Core XML file.

## Goals for this project
This project was originally concieved to validate the designs and ideas of the [OASIS OMOS Technical Committee](https://www.oasis-open.org/committees/tc_home.php?wg_abbrev=xliff-omos) but it is also hoped that it will provide a kind of reference implementation and foundation library for those wishing to evaluate JLIFF for their use cases, including, it is hoped, the GALA Localization Web Services Project.

Technical Committee outputs such as schema definitions and example serializations can be found on [GitHub](https://github.com/oasis-tcs/xliff-omos-jliff).

## Project status
As with the OASIS project itself, this project is a work in progress. I have a short attention span so some features may not be completely implmented because I got distracted by another feature during implementation.

## Latest ##
Added Builder Pattern graph construction:

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

This is only good for relatively simple graphs though.

Added Composite and Visitor Patterns so the whole graph can be traversed by creating a class that implements `ICompositeVisitor` and then calling `Process()` on the `JliffDocument.Files` property. This is how the `Segments` property is implemented.


## Branches ##
As of 2018-11-02 I have added a `dev` branch as a way of sharing bits of progress which have not been fully tested.

## Facilities
* Beginnings of a fluentgraph creation syntax see [Fluent Test Method](Jliff.Tests/XliffBookModel.cs)
* An XLIFF 2.0 filter which can be used to read XLIFF 2.0 Core XML files and raise events
* A [JliffBuilder](Jliff.Graph/Serialization/JliffBuilder.cs) which has handlers for events raised by a filter to construct a JliffDocument graph.
* Composition and Visitor Patterns.

## Licensing
The XLIFF 2.0 Object Model is licensed under the MIT License.
