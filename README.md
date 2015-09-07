# mediatag-sharp
Combines taglib-sharp and another library to provide read/write access to both WMA and MP3 tags

MediaTagSharp is intended for music-files but could be extended in-future to support more file-types. It uses taglib-sharp
to read/write tags for non-windows-media files.

To build, first add SharpZipLib to your projects directory and unzip https://github.com/icsharpcode/SharpZipLib
into it (MediaTagSharp's solution includes project ICSharpCode.SharpZLib).

Now add the project directory MediaTagSharp to your Visual Studio 2015 projects and copy the master's zip
contents into it. Navigate to src\MediaTagSharp.sln and open the solution.

It should build with one warning (from taglib-sharp)... 

Readme.txt has basic instructions on how to use MediaTagSharp. The three DLLs you want to reference or copy into
your project will be in the lib directory.

The solution targets .NET 4 but you can easily change that in the Application tab of each project's properties page.
