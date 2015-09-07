//---------------------------------------------------------------------------
// Copyright "nebulous" - For now, let's take a leap of faith and say that
// this program is distributed under the terms of the GNU General Public
// License. "I'm a doctor not a brick-layer Jim!" - Dr. McCoy
//
// I have done more work on some source files and less to none on others.
//
// Many people and groups have probably worked on this over the years
// including Microsoft's Tim Sneath - his code was what I started with.
//
// Then I found some updated source in a Windows SDK - So if you know
// anyone else I need to credit? - e-mail: dxzl@live.com (Scott Swift)
//---------------------------------------------------------------------------
// PURPOSE - to read/write Windows Format Media Meta-Data (My focus has
// been on audio-only...
//---------------------------------------------------------------------------
using System;

namespace WinMediaLib
{
	public enum MediaMetadata
	{
		// A selected list of media attributes. There are many more that aren't
		// included in this list; any additions here need to be added also to 
		// the MetadataEditor indexer this[MediaMetadata].

		// Tags in comments below come from the following URL:
		// http://msdn.com/library/en-us/wmform/htm/attributelist.asp

    AlbumArtist,          // WM/AlbumArtist
    AlbumSortOrder,       // WM/AlbumSortOrder
    AlbumTitle,           // WM/AlbumTitle
    AudioFileUrl,         // WM/AudioFileURL
    Author,               // Author
    BeatsPerMinute,       // WM/BeatsPerMinute
    BitRate,              // Bitrate
    Composer,             // WM/Composer
    Conductor,            // WM/Conductor
    ContentDistributor,   // WM/ContentDistributor
    Copyright,            // Copyright
    CopyrightUrl,         // CopyrightURL
    Description,          // Description (of Codec?)
    Duration,             // Duration
    FileSize,             // FileSize
    Genre,                // WM/Genre
    IsProtected,          // Is_Protected
    Lyrics,               // WM/Lyrics
    Provider,             // WM/Provider
    Publisher,            // WM/Publisher
    Text,                 // WM/Text
    Title,                // Title
    TrackNumber,          // WM/TrackNumber
    Year                  // WM/Year
  }
}
