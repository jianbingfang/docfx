Docfx Flavored Markdown
==========================================
Docfx supports "Docfx Flavored Markdown," or DFM. It is 100% compatiable with [Github Flavored Markdown](https://help.github.com/articles/github-flavored-markdown/), and adds some additional functionality, including cross reference and file inclusion.
### Yaml Header
Yaml header in DFM is considered as the metadata for the markdown file. It will be transformed to `yamlheader` tag when processed.
Yaml header MUST be the first thing in the file and MUST take the form of valid YAML set between triple-dashed lines. Here is a basic example:

```md
---
uid: A.md
title: A
---
```

### Cross Reference
Using `@uid` to quickly reference to other documentations.
Generally the value of `uid` can be found in the `YAML` file generated by running `docfx metadata`. For API in DOTNET world, [2.Identifiers in Metadata Dotnet Spec](metadata_dotnet_spec.md) describes the way to generate `id` for API in dotnet language. For other files, you can add YAML header at the top of the file to define its id.
```md
---
uid: A.md
---
This is a markdown file
```

### File Inclusion
DFM adds syntax to include other file parts into current file, the included file will also be considered as in DFM syntax. *NOTE* that YAML header is **NOT** supported when the file is an inclusion.
There are two types of file inclusion: inline one and block one, as similar to inline code span and block code.

#### Inline
Inline file inclusion is in the following syntax, in which `<title>` stands for the title of the included file, and `<filepath>` stands for the file path of the included file, file path can be either absolute file path or relative file path.`<filepath>` can be wrapped by `'` or `"`. *NOTE* that for inline file inclusion, the file included will be considered as containing only inline tags, e.g. `###header` inside the file will not be transformed as `<h3>` is a block tag, while `[a](b)` will be transformed to `<a href='b'>a</a>` as `<a>` is an inline tag.
```
...Other inline contents... [!include[<title>](<filepath>)]
```
#### Block
Block file inclusion must be in a single line and with no prefix characters before the start `[`. Content inside the included file will be transformed using DFM syntax.
```md
[!include[<title>](<filepath>)]
```

### Section definition
User may need to define section. Mostly used for code table. Give an example below. The attributes you write in BEGINSECTION comments will be the div tag's attributes. Currently we only allow 3 attributes, namely class, id, data-resources

    <!-- BEGINSECTION class="tabbedCodeSnippets" data-resources="OutlookServices.Calendar" -->
    ```cs-i
    var outlookClient = await CreateOutlookClientAsync("Calendar");
    var events = await outlookClient.Me.Events.Take(10).ExecuteAsync();
    foreach (var calendarEvent in events.CurrentPage)
    {
        System.Diagnostics.Debug.WriteLine("Event '{0}'.", calendarEvent.Subject);
    }
    ```
    ```javascript-i
    outlookClient.me.events.getEvents().fetch().then(function(result) {
        result.currentPage.forEach(function(event) {
        console.log('Event "' + event.subject + '"')
        });
    }, function(error)
    {
        console.log(error);
    });
    ```
    <!-- ENDSECTION -->

### CODE SNIPPET
Allow you to insert code with code language specified. The content of specified code path will be expanded.

```md
[!code-<language>[<name>](<codepath><queryoption><queryoptionvalue> "<title>")]
```

* __`<language>`__ can be made up of any number of character and '-'. However, the recommend value should follow [Highlight.js language names and aliases](http://highlightjs.readthedocs.org/en/latest/css-classes-reference.html#language-names-and-aliases).
* __`<codepath>`__ is the relative path in file system which indicates the code snippet file that you want to expand.
* __`<queryoption>`__ and __`<queryoptionvalue>`__ are used together to retrieve part of the code snippet file in the line range or tag name way. We have 2 query string options to represent these 2 ways:
    * __`#`__: _`#L{startlinenumber}-L{endlinenumber}`_ (line range) or _`#L{tagname}`_ (tag name)
    * __`?`__: _`?start={startlinenumber}&end={endlinenumber}`_ (line range) or _`?{name}={tagname}`_ (tag name)
* __`<title>`__ can be omitted.

#### Code Snippet Sample
```md
[!code-csharp[Main](Program.cs)]

[!code[Main](Program.cs#L12-L16 "This is source file")]
[!code-vb[Main](../Application/Program.vb#testsnippet "This is source file")]

[!code[Main](index.xml?start=5&end=9)]
[!code-javascript[Main](../jquery.js?name=testsnippet)]
```

#### Tag Name Representation in Code Snippet Source File
DFM currently only supports following __`<language>`__ values to be able to retrieve by tag name:
* C#: cs, csharp
* VB: vb, vbnet
* C++: cpp, c++
* F#: fsharp
* XML: xml
* Html: html
* SQL: sql
* Javascript: javascript


### Note(Warning/Tip/Important)
Using specific syntax at the beginning of block quote to indicate the following content is Note.

```md
The following text is a note types in block quote

> [!NOTE]
> <notecontent>
> <notecontent>
<notecontent>

This line is not inside the note
```