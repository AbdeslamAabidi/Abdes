Abdes
=====

Abdes API is compiling and executing scripts C# from the client side.
For reason of security and preventing changes of a script on the client side, you need to generate sha256 for your script and add it to JSON file "settings.json."

## Versions 
v0.1: Compile and execute C# Scripts

## Usage/Example:
```csharp
<script type="csharp" id="csharp" language="csharp">

    using System;

    // The namespace, class name and the method name shouldn't change
    namespace Abdes
    {
        public class  Program
        {
            public string Main()
            {
                return "Hello, World!";
            }
        }
    }

</script>
```
```js
$.ajax({
            type: "GET",
            url: "http://{localhost}/api/abdes",
            data:
            {
                language: $("#csharp").attr('language'),
                code: $("#csharp").html()
            },
            success: function (results) {
                if (results.status == 0) {
                    console.log(results);
                    $('#data').html('Result: ' + results.data);
                }
                else {
                    console.log('Request failed.  Returned message: ' + results.message);
                }
            },
            fail: function (data, status) {
                console.log('Request failed.  Returned status of ' + status);
            }
        });
```

## Tests:
This has been compiled and tested successfully on visual studio 2017 .net core 2.2
	
## The MIT License (MIT)

Copyright (c) 2018 Abdsoft

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
