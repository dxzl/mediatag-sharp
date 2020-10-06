# mediatag-sharp
Combines taglib-sharp and another library to provide read/write access to both WMA and MP3 music tags. MediaTagSharp can write WMA tags.

MediaTagSharp uses a modified version of taglib-sharp to read/write non-WMA tags.

To build:
Download mediatag-sharp and unzip to your Visual Studio projects directory.
Download SharpZipLib and unzip to your Visual Studio projects directory. https://github.com/icsharpcode/SharpZipLib

Navigate to src\MediaTagSharp.sln and open the solution. There are four projects.

The signing-file, DtsUDP.pfx will show with a yellow caution. You can substitute your own signing-file or right-click and delete it. Open each project's properties page, click the Signing tab and uncheck Sign The Assembly. The solution targets .NET 4 but you can easily change that in the Application tab of each project's properties page.

Choose Build->Clean, then Build->Build. The three DLLs you want to reference or copy into your project will be in the lib directory.

Readme.txt has basic instructions on how to use MediaTagSharp. 
