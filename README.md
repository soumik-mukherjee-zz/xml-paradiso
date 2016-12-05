# Background

### What this example is about
XML, and file based integrations, are not yet a thing of the past. And while they used to pose "happy" challenges to developers, the toolset community has forever evolved to reduce the "burden" of technology on us. Today we will be exploring a particular problem set:
- A B2B integration scenario which begins with the definition of a data model, often in the form of a XSD, in the XML world. Developers will have little or no control on how the XSD is defined but expected to write code that produces XML messages adhereing to the XSD. This approach is often referred to a "Contract First" (the opposite being "Code first").
- With a traditional XML programming paradigm, developer required polygot skills - one set focussing on the core platform (e.g. .NET) and the other on XML APIs and specs (e.g. the well known [W3C DOM](https://www.w3.org/DOM/)).
- While toolsets have reduced this burden, there are competeing products, often from the same vendor, with frequently changing roadmaps. e.g. .NET Framework DataContractSerializer vs XmlSerializer (there is a good read on the subject [here](http://www.cnblogs.com/asingna/archive/2011/11/06/2237904.html)). Amongst the pitfalls, is the risk of your code falling out of vendor support.
- Toolsets automating the "Code first" workflow are widely popular, while those in the "Contract First" world, not as much. This has less to do with the fact of the toolset's maturity, and more to do with the natural preference for a "Code first" approach in a majority of cases. However this problem magnifies when "Code First" is not an option (e.g. B2B integrations)

Throughout this example, we will try to address the above problems, with a certain choice of toolset, i.e. WCF & [DataContractSerializer](https://msdn.microsoft.com/en-us/library/ms731073(v=vs.110).aspx)
- We will start with a XSD that has already been defined
- See how WCF toolsets can automate DataContract generation, specifically `svcutil` ([MSDN link](https://msdn.microsoft.com/en-us/library/aa347733(v=vs.110).aspx)). This may not seem an obvious choice, but `svcutil` can go beyond its percieved role. The competing toolset (which is already losing favor from within MS) would be `Xsd.exe` ([MSDN link](https://msdn.microsoft.com/en-us/library/x6c1kb0s(v=vs.110).aspx))
- While "Contract First" has out of box support with `svcutil` on .NET 4.5+, its not the end of the world for earthlings on .NET 4.0. We will explore how we can achieve this.
- How the DataContract, once "defined", eases XML generation to a huge extent

### The development workflow
We are dealing with a scenario, where data needs to be fetched/processed/transformed, resulting in .NET objects, which are then finally transformed in XML. The concern of transport, i.e. file, is rather trivial, compared to the rest. The diagram below shows the interaction between components that we will "develop", and their sequence of development within the worlflow.
```
                                         [1.XSD]  
                                           |
                               (Generate)  |<-----------------> svcutil
                                           |
                                           Y
  [Plain Old C# Objects]------------>[2.DataContract]------------->[XML]
                             Y                             Y
                             |                             |
                             |                             |
                [3.Business Logic in .NET]   [4.Serialization Logic in .NET]
```

### Putting it to use - sample code

Use [this](blogs/ContractFirstDotnet.md) link to use a sample code. You will also find instructions on how to build and run it.
