MediaTagSharp is intended for music-files but could be extended in-future to support more file-types. It uses taglib-sharp
to read/write tags for non-windows-media files.

MediaTagSharp uses the same data-structure for either mp3 or windows media files (WMA, Etc.). You supply the file-name
and the information comes back

This C# dll lets you easily read/write music-file tags for both .wma and .mp3 files. Its advantage over the Microsoft Media Foundation is that it works with Windows XP too :)

The Solution file is in the "src" directory and works ok with Visual Studio 2010 or 2015).

After building, the three output library files for .NET 4 will be copied to the "lib" directory. You can compile for
either x86 or x64.

MediaTags.dll depends on the other two libraries, TaglibSharp.dll and WinMediaLib.dll.

-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------

To use:

1) Copy all three DLLs into your own Visual Studio project and create references to them by right-clicking References, Add Reference, click the Browse tab and select the dll file to add. Click the newly added reference and set "Copy Local" to true to copy it to your output directory... 

2) Include the using statements:

    using MediaTags;
    using WinMediaLib; // optional
    using TagLib; // optional

3) Create a new instance of MediaTags to read/write SongInfo:

   var mt = new MediaTags.MediaTags();

4) Read a file's music tags with:

   SongInfo si = mt.Read(fileString);

5) Change some info... say the Album name and set a flag to notify the library to update that field:

   si.Album = "My New Album";
   si.bAlbumTag = true;

6) Write the info back:

   mt.Write(si, fileString);

*note: I use the file's extention to determine how to access its information. Files with the extension ".wma" ".asf" ".wmv" ".wm" are written using WinMediaLib.dll. All other extensions use TaglibSharp.dll.

-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------

Credits: This project was derived from two sources:

A. Taglib Sharp: https://github.com/mono/taglib-sharp (the only change I made in taglib-sharp was to add access to
the "Publisher" tag).

B. Windows Media Format 11 SDK: http://msdn.microsoft.com/en-us/library/windows/desktop/dd757738(v=vs.85).aspx

With thanks to Tim Sneith of Microsoft for his old MediaCatalog project which got me started... :)

Taglib-sharp modification (from stackoverflow.com):

  Open the file TagLib/Id3v2/FrameTypes.cs and add the following line somewhere:

    public static readonly ReadOnlyByteVector TPUB = "TPUB";  // Publisher field

  And in the file TagLib/Id3v2/Tag.cs:

    public string Publisher
    {
      get {return GetTextAsString (FrameType.TPUB);}
      set {SetTextFrame (FrameType.TPUB, value)};
    }

  You can then access the Publisher field using something like this:

    TagLib.File tf = TagLib.File.Create(...);   // open file
    tf.Tag.Publisher = "Label XY";              // write new Publisher
    tf.Save();                                  // save tags

Contact me at: dxzl@live.com

