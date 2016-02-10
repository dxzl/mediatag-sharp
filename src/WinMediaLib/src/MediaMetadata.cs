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
    AcoustID,             // AcoustID
    MBID,                 // MBID (MusicBrainz)
    Provider,             // WM/Provider
    Publisher,            // WM/Publisher
    Text,                 // WM/Text
    Title,                // Title
    TrackNumber,          // WM/TrackNumber
    Year                  // WM/Year
  }
}
