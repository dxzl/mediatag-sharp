using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using WinMediaLib;
using TagLib;

namespace MediaTags
{
  //---------------------------------------------------------------------------
  public class WmTypes
  {
    public const ushort WM_TYPE_DWORD = 0;
    public const ushort WM_TYPE_STRING = 1;
    public const ushort WM_TYPE_BINARY = 2;
    public const ushort WM_TYPE_BOOL = 3;
    public const ushort WM_TYPE_QWORD = 4;
    public const ushort WM_TYPE_WORD = 5;
    public const ushort WM_TYPE_GUID = 6;
  }
  //---------------------------------------------------------------------------
  // Declare the SongInfo class:
  public class SongInfo
  {
    // CAN'T WRITE THESE in taglib-sharp because they are read-only!
    // Description
    // AudioBitrate
    // AudioChannels
    // BitsPerSample
    // AudioSampleRate
    // FileSize

    // Song file-types
    public const int UNDEFINED = -1;
    public const int FT_WMA = 0; // File-type is WMA
    public const int FT_MP3 = 1; // File-type is MP3

    // Common fields wma, mp3
    public string Title = string.Empty;
    public string Artist = string.Empty;
    public string Performer = string.Empty;
    public string Album = string.Empty;
    public string Composer = string.Empty;
    public string Genre = string.Empty;
    public string Duration = string.Empty;
    public string Comments = string.Empty; // We will use WM/Text field of .wma for Comments!
    public string Lyrics = string.Empty;
    public string Conductor = string.Empty;
    public string Publisher = string.Empty;
    public string Track = string.Empty;
    public string Year = string.Empty;

    public string FilePath = string.Empty;

    public int FileType = UNDEFINED; // -1 no tag info, 0 wma file, 1 mp3 file
    public bool bException = false; // Threw an exception?

    public string Copyright = string.Empty;
    public int TrackCount = UNDEFINED;
    public int Disc = UNDEFINED;
    public int DiscCount = UNDEFINED;

    // Additional Info (may not be available for all media file-types!)

    // Read Only!
    public long FileSize = UNDEFINED;
    public int BitRate = UNDEFINED;
    public int Channels = UNDEFINED;
    public int BitsPerSample = UNDEFINED;
    public int SampleRate = UNDEFINED;
    public string Codec = string.Empty;
    public string Description = string.Empty; // has "MPEG Version 1 Audio, Layer 3"

    // Windows Media Only
    public bool bProtected = false;

    // Flags indicating that we have info present for the corresponding field...
    // If there is no info for title, album or artist we try to fill them in by
    // disecting the song's file path, but the flag is set to false.
    public bool bTitleTag = false;
    public bool bArtistTag = false;
    public bool bPerformerTag = false;
    public bool bAlbumTag = false;
    public bool bComposerTag = false;
    public bool bGenreTag = false;
    public bool bDurationTag = false;
    public bool bCommentsTag = false;
    public bool bLyricsTag = false;
    public bool bConductorTag = false;
    public bool bPublisherTag = false;
    public bool bTrackTag = false;
    public bool bYearTag = false;

    // Additional
    public bool bCopyrightTag = false;
    public bool bBitrateTag = false;
    public bool bDescriptionTag = false;
    public bool bFilesizeTag = false;
    public bool bFilepathTag = false;

    // Not applicable to Windows Media files...
    public bool bTrackCountTag = false;
    public bool bDiscTag = false;
    public bool bDiscCountTag = false;
    //---------------------------------------------------------------------------
    // Constructor
    public SongInfo()
    {
      ClearAll(); // Initialize
    }
    //---------------------------------------------------------------------------
    public void TrimAll()
    {
      try
      {
        Title = Title.Trim();
        Artist = Artist.Trim();
        Performer = Performer.Trim();
        Album = Album.Trim();
        Composer = Composer.Trim();
        Genre = Genre.Trim();
        Duration = Duration.Trim();
        Comments = Comments.Trim();
        Lyrics = Lyrics.Trim();
        Conductor = Conductor.Trim();
        Publisher = Publisher.Trim();
        Track = Track.Trim();
        Year = Year.Trim();
        Copyright = Copyright.Trim();
      }
      catch
      {
      }
    }
    //---------------------------------------------------------------------------
    public bool AnyMainFlags()
    {
      return bTitleTag || bArtistTag || bPerformerTag || bAlbumTag || bComposerTag ||
        bGenreTag || bDurationTag || bCommentsTag || bLyricsTag || bConductorTag ||
        bPublisherTag || bTrackTag || bYearTag || bCopyrightTag ? true : false;
    }
    //---------------------------------------------------------------------------
    public void ClearAll()
    {
      try
      {
        // Main
        Title =
        Artist =
        Performer =
        Album =
        Composer =
        Genre =
        Duration =
        Comments =
        Lyrics =
        Conductor =
        Publisher =
        Track =
        Year =
        Copyright = string.Empty;

        // Additional
        FilePath =
        Codec =
        Description = string.Empty;

        bTitleTag =
        bArtistTag =
        bPerformerTag =
        bAlbumTag =
        bComposerTag =
        bGenreTag =
        bDurationTag =
        bCommentsTag =
        bLyricsTag =
        bConductorTag =
        bPublisherTag =
        bTrackTag =
        bYearTag =
        bCopyrightTag = false;

        bFilepathTag =
        bDescriptionTag =
        bTrackCountTag =
        bDiscTag =
        bDiscCountTag =
        bBitrateTag =
        bFilesizeTag =
        bException =
        bProtected = false;

        TrackCount =
        Disc =
        DiscCount =
        BitRate =
        Channels =
        BitsPerSample =
        SampleRate =
        FileType = UNDEFINED;

        FileSize = UNDEFINED;
      }
      catch { }
    }
  }
  //---------------------------------------------------------------------------
  // Short version (for speed)
  public struct SongInfo2
  {
    // Song file-types
    public const int UNDEFINED = -1;
    public const int FT_WMA = 0; // File-type is WMA
    public const int FT_MP3 = 1; // File-type is MP3

    // Common fields wma, mp3
    public string Title;
    public string Artist;
    public string Album;
    public string Performer;
    public string Duration;
    public string FilePath;

    public int FileType; // -1 no tag info, 0 wma file, 1 mp3 file
    public bool bException; // Threw an exception?

    // Flags indicating that we have info present for the corresponding field...
    // If there is no info for title, album or artist we try to fill them in by
    // disecting the song's file path, but the flag is set to false.
    public bool bTitleTag;
    public bool bArtistTag;
    public bool bAlbumTag;
    public bool bPerformerTag;
    public bool bDurationTag;
    public bool bFilepathTag;

    //---------------------------------------------------------------------------
    public static void init(SongInfo2 si)
    {
      si.Title = "";
      si.Artist = "";
      si.Performer = "";
      si.Album = "";
      si.Duration = "";
      si.FilePath = "";
      si.FileType = UNDEFINED;
      si.bException = false;
      si.bTitleTag = false;
      si.bArtistTag = false;
      si.bAlbumTag = false;
      si.bPerformerTag = false;
      si.bDurationTag = false;
      si.bFilepathTag = false;
    }
  }
  //---------------------------------------------------------------------------
  public class MediaTags : IDisposable
  {
    #region Properties

    private string g_root;
    public string Root
    {
      get { return this.g_root; }
      set { this.g_root = value; }
    }
    //---------------------------------------------------------------------------
    #endregion

    // Windows Media types:
    public const ushort WMT_TYPE_DWORD = 0;
    public const ushort WMT_TYPE_STRING = 1;
    public const ushort WMT_TYPE_BINARY = 2;
    public const ushort WMT_TYPE_BOOL = 3;
    public const ushort WMT_TYPE_QWORD = 4;
    public const ushort WMT_TYPE_WORD = 5;
    public const ushort WMT_TYPE_GUID = 6;

    //---------------------------------------------------------------------------
    public void Dispose()
    {
     // We have no global resources...
    }
    //---------------------------------------------------------------------------
    public MediaTags(string root)
    {
      this.g_root = root;
    }
    //---------------------------------------------------------------------------
    public MediaTags()
    {
      this.g_root = string.Empty;
    }
    //---------------------------------------------------------------------------
    public static string Version()
    // Returns the Assembly's version string
    {
      return Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
    //---------------------------------------------------------------------------
    public bool Write(SongInfo si, string file)
    // Write a media-file's metadata (tags) from a SongInfo struct...
    // Returns true if success.
    {
      string ext = Path.GetExtension(file).ToLower();

      bool bRet = false; // Presume error

      if (ext == ".wma" || ext == ".asf" || ext == ".wmv" || ext == ".wm")
      {
        try
        {
          // Attribute types:
          // WMT_TYPE_DWORD = 0;
          // WMT_TYPE_STRING = 1;
          // WMT_TYPE_BINARY = 2;
          // WMT_TYPE_BOOL = 3;
          // WMT_TYPE_QWORD = 4;
          // WMT_TYPE_WORD = 5;
          // WMT_TYPE_GUID = 6;

          using (MediaDataManager Editor = new MediaDataManager()) // WMA Editor...
          {
            // See the end of this file for a list of windows media attributes...
            if (si.bAlbumTag) Editor.SetAttrib(file, 0, "WM/AlbumTitle", MediaTags.WMT_TYPE_STRING, si.Album);
            if (si.bTitleTag) Editor.SetAttrib(file, 0, "Title", MediaTags.WMT_TYPE_STRING, si.Title);
            if (si.bArtistTag) Editor.SetAttrib(file, 0, "WM/AlbumArtist", MediaTags.WMT_TYPE_STRING, si.Artist);
            if (si.bPerformerTag) Editor.SetAttrib(file, 0, "Author", MediaTags.WMT_TYPE_STRING, si.Performer);
            if (si.bCommentsTag) Editor.SetAttrib(file, 0, "WM/Text", MediaTags.WMT_TYPE_STRING, si.Comments);
            if (si.bGenreTag) Editor.SetAttrib(file, 0, "WM/Genre", MediaTags.WMT_TYPE_STRING, si.Genre);
            if (si.bPublisherTag) Editor.SetAttrib(file, 0, "WM/Publisher", MediaTags.WMT_TYPE_STRING, si.Publisher);
            if (si.bComposerTag) Editor.SetAttrib(file, 0, "WM/Composer", MediaTags.WMT_TYPE_STRING, si.Composer);
            if (si.bConductorTag) Editor.SetAttrib(file, 0, "WM/Conductor", MediaTags.WMT_TYPE_STRING, si.Conductor);
            if (si.bYearTag) Editor.SetAttrib(file, 0, "WM/Year", MediaTags.WMT_TYPE_STRING, si.Year);
            if (si.bLyricsTag) Editor.SetAttrib(file, 0, "WM/Lyrics", MediaTags.WMT_TYPE_STRING, si.Lyrics);

            // Additional Info:
            if (si.bCopyrightTag) Editor.SetAttrib(file, 0, "Copyright", MediaTags.WMT_TYPE_STRING, si.Copyright);
            if (si.bDescriptionTag) Editor.SetAttrib(file, 0, "Description", MediaTags.WMT_TYPE_STRING, si.Description);

            try
            {
              if (si.bTrackTag) Editor.SetAttrib(file, 0, "WM/TrackNumber", MediaTags.WMT_TYPE_DWORD, si.Track);
              if (si.bBitrateTag) Editor.SetAttrib(file, 0, "Bitrate", MediaTags.WMT_TYPE_DWORD, si.BitRate.ToString());
              if (si.bFilesizeTag) Editor.SetAttrib(file, 0, "FileSize", MediaTags.WMT_TYPE_QWORD, si.FileSize.ToString());
            }
            catch
            {
            }

            // These don't exist for windows media files...
            // .TrackCount = (uint)si.TrackCount;
            // .Disc = (uint)si.Disc;
            // .DiscCount = (uint)si.DiscCount;
            // .AudioChannels = si.Channels;
            // .BitsPerSample = si.BitsPerSample;
            // .AudioSampleRate = si.SampleRate;

            bRet = true;
          } // end using
        }
        catch
        {
        }
      }
      else try
      {
        using (TagLib.File f = TagLib.File.Create(file)) // Use taglib-sharp...
        {
          // Get the tag-frame and create it if necessary...
          TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag)f.GetTag(TagLib.TagTypes.Id3v2, true);

          if (tag == null) return false;

          if (si.bAlbumTag) tag.Album = si.Album;
          if (si.bTitleTag) tag.Title = si.Title;
          if (si.bCommentsTag) tag.Comment = si.Comments;

          if (si.bGenreTag)
          {
            // Get list of artists, if any, and re-write
            string[] Genres = tag.Genres;

            if (Genres.Length == 0)
              tag.Genres = new string[] { si.Genre };
            else
            {
              Genres[0] = si.Genre;
              tag.Genres = Genres;
            }
          }

          if (si.bComposerTag)
          {
            // Get list of artists, if any, and re-write
            string[] Composers = tag.Composers;

            if (Composers.Length == 0) tag.Composers = new string[] { si.Composer };
            else
            {
              Composers[0] = si.Composer;
              tag.Composers = Composers;
            }
          }

          if (si.bArtistTag)
          {
            // Get list of artists, if any, and re-write
            string[] Artists = tag.AlbumArtists;

            if (Artists.Length == 0) tag.AlbumArtists = new string[] { si.Artist };
            else
            {
              Artists[0] = si.Artist;
              tag.AlbumArtists = Artists;
            }
          }

          if (si.bPerformerTag)
          {
            // Get list of performers, if any, and re-write
            string[] Performers = tag.Performers;

            if (Performers.Length == 0) tag.Performers = new string[] { si.Performer };
            else
            {
              Performers[0] = si.Performer;
              tag.Performers = Performers;
            }
          }

          if (si.bPublisherTag) tag.Publisher = si.Publisher;

          if (si.bConductorTag) tag.Conductor = si.Conductor;

          if (si.bYearTag)
          {
            try { tag.Year = Convert.ToUInt32(si.Year); }
            catch { tag.Year = 0; }
          }

          if (si.bTrackTag)
          {
            try { tag.Track = Convert.ToUInt32(si.Track); }
            catch { tag.Track = 0; }
          }

          // Additional Info:
          if (si.bLyricsTag) tag.Lyrics = si.Lyrics;

          if (si.bCopyrightTag) tag.Copyright = si.Copyright;
          if (si.bTrackCountTag) tag.TrackCount = (uint)si.TrackCount;
          if (si.bDiscTag) tag.Disc = (uint)si.Disc;
          if (si.bDiscCountTag) tag.DiscCount = (uint)si.DiscCount;

          // CAN'T SET THESE in taglib-sharp because they are read-only!
          //f.Properties.Description = si.Description;
          //f.Properties.AudioBitrate = si.BitRate;
          //f.Properties.AudioChannels = si.Channels;
          //f.Properties.BitsPerSample = si.BitsPerSample;
          //f.Properties.AudioSampleRate = si.SampleRate;
          //f.Length = si.FileSize;

          tag.CopyTo(f.Tag, true); // Overwrite the song file's tag...
          f.Save(); // Save tag

          bRet = true;
        } // end using
      }
      catch { }

      return bRet;
    }
    //---------------------------------------------------------------------------
    public bool Write2(SongInfo2 si, string file)
    // Write a media-file's metadata (tags) from a SongInfo2 struct...
    // Returns true if success.
    {
      string ext = Path.GetExtension(file).ToLower();

      bool bRet = false; // Presume error

      if (ext == ".wma" || ext == ".asf" || ext == ".wmv" || ext == ".wm")
      {
        try
        {
          using (MediaDataManager Editor = new MediaDataManager()) // WMA Editor...
          {

            // See the end of this file for a list of windows media attributes...
            if (si.bAlbumTag) Editor.SetAttrib(file, 0, "WM/AlbumTitle", MediaTags.WMT_TYPE_STRING, si.Album);
            if (si.bTitleTag) Editor.SetAttrib(file, 0, "Title", MediaTags.WMT_TYPE_STRING, si.Title);
            if (si.bArtistTag) Editor.SetAttrib(file, 0, "WM/AlbumArtist", MediaTags.WMT_TYPE_STRING, si.Artist);
            if (si.bPerformerTag) Editor.SetAttrib(file, 0, "Author", MediaTags.WMT_TYPE_STRING, si.Performer);

            bRet = true;
          } // end using
        }
        catch { }
      }
      else try
      {
        using (TagLib.File f = TagLib.File.Create(file)) // Use taglib-sharp...
        {
          // Get the tag-frame and create it if necessary...
          TagLib.Id3v2.Tag tag = (TagLib.Id3v2.Tag)f.GetTag(TagLib.TagTypes.Id3v2, true);

          if (tag == null) return false;

          if (si.bAlbumTag) tag.Album = si.Album;
          if (si.bTitleTag) tag.Title = si.Title;

          if (si.bArtistTag)
          {
            // Get list of artists, if any, and re-write
            string[] Artists = tag.AlbumArtists;

            if (Artists.Length == 0) tag.AlbumArtists = new string[] { si.Artist };
            else
            {
              Artists[0] = si.Artist;
              tag.AlbumArtists = Artists;
            }
          }

          if (si.bPerformerTag)
          {
            // Get list of performers, if any, and re-write
            string[] Performers = tag.Performers;

            if (Performers.Length == 0) tag.Performers = new string[] { si.Performer };
            else
            {
              Performers[0] = si.Performer;
              tag.Performers = Performers;
            }
          }

          tag.CopyTo(f.Tag, true); // Overwrite the song file's tag...
          f.Save(); // Save tag

          bRet = true;
        } // end using
      }
      catch { }

      return bRet;
    }
    //---------------------------------------------------------------------------
    public SongInfo Read(string file)
    // Read a media-file's metadata (tags) into a SongInfo struct...
    {
      string ext = Path.GetExtension(file).ToLower();
      SongInfo si = new SongInfo();

      si.FilePath = file;

      if (ext == ".wma" || ext == ".asf" || ext == ".wmv" || ext == ".wm")
      {
        try
        {
          si.FileType = SongInfo.FT_WMA; // wma tag info

          MediaDataManager mdm = new MediaDataManager();

          if (mdm == null)
          {
            si.bException = true; // Error
            return si;
          }

          TrackInfo ti;

          try { ti = mdm.RetrieveTrackInfo(file); }
          catch
          {
            si.bException = true;
            return si;
          }

          si.Duration = ti.Duration.ToString(); if (!string.IsNullOrEmpty(si.Duration)) si.bDurationTag = true;
          if (ti.Title != null) { si.Title = ti.Title.Trim(); if (!string.IsNullOrEmpty(si.Title)) si.bTitleTag = true; }
          if (ti.AlbumArtist != null) { si.Artist = ti.AlbumArtist.Trim(); if (!string.IsNullOrEmpty(si.Artist)) si.bArtistTag = true; }

          // ti.Author: The Author attribute is the name of a media artist or actor associated with the content.
          // This attribute may have multiple values. To retrieve all of the values for a multi-valued attribute, you must use
          // the Media.getItemInfoByType method, not the Media.getItemInfo (WinMediaLib project) method.
          // Same as FirstPerformer in a mp3 file
          if (ti.Author != null) { si.Performer = ti.Author.Trim(); if (!string.IsNullOrEmpty(si.Performer)) si.bPerformerTag = true; }

          if (ti.AlbumTitle != null) { si.Album = ti.AlbumTitle.Trim(); if (!string.IsNullOrEmpty(si.Album)) si.bAlbumTag = true; }
          if (ti.Composer != null) { si.Composer = ti.Composer.Trim(); if (!string.IsNullOrEmpty(si.Composer)) si.bComposerTag = true; }
          if (ti.Genre != null) { si.Genre = ti.Genre.Trim(); if (!string.IsNullOrEmpty(si.Genre)) si.bGenreTag = true; }
          if (ti.Text != null) { si.Comments = ti.Text.Trim(); if (!string.IsNullOrEmpty(si.Comments)) si.bCommentsTag = true; }
          if (ti.Lyrics != null) { si.Lyrics = ti.Lyrics.Trim(); if (!string.IsNullOrEmpty(si.Lyrics)) si.bLyricsTag = true; }
          if (ti.Conductor != null) { si.Conductor = ti.Conductor.Trim(); if (!string.IsNullOrEmpty(si.Conductor)) si.bConductorTag = true; }
          if (ti.Publisher != null) { si.Publisher = ti.Publisher.Trim(); if (!string.IsNullOrEmpty(si.Publisher)) si.bPublisherTag = true; }
          if (ti.Year != null) { si.Year = ti.Year.Trim(); if (!string.IsNullOrEmpty(si.Year)) si.bYearTag = true; }
          si.Track = ti.Track.ToString(); if (!string.IsNullOrEmpty(si.Track)) si.bTrackTag = true;

          // Extra info...
          si.FileSize = (long)ti.FileSize; if (si.FileSize >= 0) si.bFilesizeTag = true;
          si.BitRate = (int)ti.BitRate; if (si.BitRate >= 0) si.bBitrateTag = true;
          si.bProtected = ti.IsProtected;
        }
        catch
        {
          si.ClearAll();
          si.bException = true; // Threw an exception
        }
      }
      else using (TagLib.File tagFile = TagLib.File.Create(file)) // Use taglib-sharp...
      {
        // Note - checking Length on a null string throws an exception!

        // Snippet to read rating sio... (not sure if for mp3 or wma!) but cool :)
        //
        //TagLib.Tag tag123 = tagsio.GetTag(TagTypes.Id3v2);
        //var usr = "Windows Media Player 9 Series";
        //TagLib.Id3v2.PopularimeterFrame frame = TagLib.Id3v2.PopularimeterFrame.Get(
        //                                       (TagLib.Id3v2.Tag)tag123, usr, true);
        //ulong PlayCount = frame.PlayCount;
        //int Rating = frame.Rating;

        // To WRITE:
        //Id3v2.Tag tag = (Id3v2.Tag)file.GetTag(TagTypes.Id3v2, true); // set create flag
        //tag.SetTextFrame(FrameType.TRCK, "03");

        try
        {
          //TagLib.dll Usage:
          //
          //TagLib.File tagFile = TagLib.File.Create(mp3file);
          //uint year = tagFile.Tag.Year;
          //
          //You set the tags like this:
          //tagFile.Tag.Year = year;
          //
          //And then save the changes:
          //tagFile.Save();

          si.FileType = SongInfo.FT_MP3; // mp3 tag info

          if (tagFile == null)
          {
            si.bException = true; // Error
            return si;
          }

          si.Duration = tagFile.Properties.Duration.ToString(); if (!string.IsNullOrEmpty(si.Duration)) si.bDurationTag = true;
          if (tagFile.Properties.Description != null) { si.Description = tagFile.Properties.Description.Trim(); if (!string.IsNullOrEmpty(si.Description)) si.bDescriptionTag = true; }
          si.BitRate = tagFile.Properties.AudioBitrate; si.bBitrateTag = true;
          si.FileSize = tagFile.Length; si.bFilesizeTag = true;

          // add flags???
          si.Channels = tagFile.Properties.AudioChannels;
          si.BitsPerSample = tagFile.Properties.BitsPerSample;
          si.SampleRate = tagFile.Properties.AudioSampleRate;

          // Try to read the v2 tag-frame...
          TagLib.Id3v2.Tag tagv2 = (TagLib.Id3v2.Tag)tagFile.GetTag(TagLib.TagTypes.Id3v2);

          if (tagv2 != null)
          {
            if (tagv2.Title != null) { si.Title = tagv2.Title.Trim(); if (!string.IsNullOrEmpty(si.Title)) si.bTitleTag = true; }
            if (tagv2.FirstAlbumArtist != null) { si.Artist = tagv2.FirstAlbumArtist.Trim(); if (!string.IsNullOrEmpty(si.Artist)) si.bArtistTag = true; }
            if (tagv2.FirstPerformer != null) { si.Performer = tagv2.FirstPerformer.Trim(); if (!string.IsNullOrEmpty(si.Performer)) si.bPerformerTag = true; }  // Same as Author in a wma file
            if (tagv2.FirstComposer != null) { si.Composer = tagv2.FirstComposer.Trim(); if (!string.IsNullOrEmpty(si.Composer)) si.bComposerTag = true; }
            if (tagv2.Album != null) { si.Album = tagv2.Album.Trim(); if (!string.IsNullOrEmpty(si.Album)) si.bAlbumTag = true; }
            if (tagv2.FirstGenre != null) { si.Genre = tagv2.FirstGenre.Trim(); if (!string.IsNullOrEmpty(si.Genre)) si.bGenreTag = true; }
            if (tagv2.Comment != null) { si.Comments = tagv2.Comment.Trim(); if (!string.IsNullOrEmpty(si.Comments)) si.bCommentsTag = true; }
            if (tagv2.Lyrics != null) { si.Lyrics = tagv2.Lyrics.Trim(); if (!string.IsNullOrEmpty(si.Lyrics)) si.bLyricsTag = true; }
            if (tagv2.Conductor != null) { si.Conductor = tagv2.Conductor.Trim(); if (!string.IsNullOrEmpty(si.Conductor)) si.bConductorTag = true; }
            if (tagv2.Publisher != null) { si.Publisher = tagv2.Publisher.Trim(); if (!string.IsNullOrEmpty(si.Publisher)) si.bPublisherTag = true; }
            if (tagv2.Year > 0) { si.Year = tagv2.Year.ToString(); if (!string.IsNullOrEmpty(si.Year)) si.bYearTag = true; }
            if (tagv2.Track > 0) { si.Track = tagv2.Track.ToString(); if (!string.IsNullOrEmpty(si.Track)) si.bTrackTag = true; }
            if (tagv2.Copyright != null) { si.Copyright = tagv2.Copyright.Trim(); if (!string.IsNullOrEmpty(si.Copyright)) si.bCopyrightTag = true; }

            // Extra info
            si.TrackCount = (int)tagv2.TrackCount;
            si.Disc = (int)tagv2.Disc;
            si.DiscCount = (int)tagv2.DiscCount;
          }
          else // Try to read the old v1 tag
          {
            // Try to read the v1 tag-frame...
            TagLib.Id3v1.Tag tagv1 = (TagLib.Id3v1.Tag)tagFile.GetTag(TagLib.TagTypes.Id3v1);

            if (tagv1 == null)
            {
              si.ClearAll();
              si.bException = true; // Error
              return si;
            }

            if (tagv1.Title != null) { si.Title = tagv1.Title.Trim(); if (!string.IsNullOrEmpty(si.Title)) si.bTitleTag = true; }
            if (tagv1.FirstAlbumArtist != null) { si.Artist = tagv1.FirstAlbumArtist.Trim(); if (!string.IsNullOrEmpty(si.Artist)) si.bArtistTag = true; }
            if (tagv1.FirstPerformer != null) { si.Performer = tagv1.FirstPerformer.Trim(); if (!string.IsNullOrEmpty(si.Performer)) si.bPerformerTag = true; }  // Same as Author in a wma file
            if (tagv1.FirstComposer != null) { si.Composer = tagv1.FirstComposer.Trim(); if (!string.IsNullOrEmpty(si.Composer)) si.bComposerTag = true; }
            if (tagv1.Album != null) { si.Album = tagv1.Album.Trim(); if (!string.IsNullOrEmpty(si.Album)) si.bAlbumTag = true; }
            if (tagv1.FirstGenre != null) { si.Genre = tagv1.FirstGenre.Trim(); if (!string.IsNullOrEmpty(si.Genre)) si.bGenreTag = true; }
            if (tagv1.Comment != null) { si.Comments = tagv1.Comment.Trim(); if (!string.IsNullOrEmpty(si.Comments)) si.bCommentsTag = true; }
            if (tagv1.Lyrics != null) { si.Lyrics = tagv1.Lyrics.Trim(); if (!string.IsNullOrEmpty(si.Lyrics)) si.bLyricsTag = true; }
            if (tagv1.Conductor != null) { si.Conductor = tagv1.Conductor.Trim(); if (!string.IsNullOrEmpty(si.Conductor)) si.bConductorTag = true; }
            if (tagv1.Year > 0) { si.Year = tagv1.Year.ToString(); if (!string.IsNullOrEmpty(si.Year)) si.bYearTag = true; }
            if (tagv1.Track > 0) { si.Track = tagv1.Track.ToString(); if (!string.IsNullOrEmpty(si.Track)) si.bTrackTag = true; }
            if (tagv1.Copyright != null) { si.Copyright = tagv1.Copyright.Trim(); if (!string.IsNullOrEmpty(si.Copyright)) si.bCopyrightTag = true; }

            // Extra info
            si.TrackCount = (int)tagv1.TrackCount;
            si.Disc = (int)tagv1.Disc;
            si.DiscCount = (int)tagv1.DiscCount;
          }
        }
        catch
        {
          si.ClearAll();
          si.bException = true; // Threw an exception
        }
      }

      // Trim it up
      si.TrimAll();
      return si;
    }
    //---------------------------------------------------------------------------
    public SongInfo2 Read2(string file)
    // Read a media-file's metadata2 (tags) into a simple SongInfo2 struct that
    // uses little memory... use a static method to initialize it.
    {
      string ext = Path.GetExtension(file).ToLower();
      SongInfo2 si = new SongInfo2();
      SongInfo2.init(si);

      si.FilePath = file;

      if (ext == ".wma" || ext == ".asf" || ext == ".wmv" || ext == ".wm")
      {
        try
        {
          si.FileType = SongInfo2.FT_WMA; // wma tag info

          MediaDataManager mdm = new MediaDataManager();

          if (mdm == null)
          {
            si.bException = true; // Error
            return si;
          }

          TrackInfo ti;

          try { ti = mdm.RetrieveTrackInfo(file); }
          catch
          {
            si.bException = true;
            return si;
          }

          si.Duration = ti.Duration.ToString(); if (!string.IsNullOrEmpty(si.Duration)) si.bDurationTag = true;
          if (ti.Title != null) { si.Title = ti.Title.Trim(); if (!string.IsNullOrEmpty(si.Title)) si.bTitleTag = true; }
          if (ti.AlbumArtist != null) { si.Artist = ti.AlbumArtist.Trim(); if (!string.IsNullOrEmpty(si.Artist)) si.bArtistTag = true; }

          // ti.Author: The Author attribute is the name of a media artist or actor associated with the content.
          // This attribute may have multiple values. To retrieve all of the values for a multi-valued attribute, you must use
          // the Media.getItemInfoByType method, not the Media.getItemInfo (WinMediaLib project) method.
          // Same as FirstPerformer in a mp3 file
          if (ti.Author != null) { si.Performer = ti.Author.Trim(); if (!string.IsNullOrEmpty(si.Performer)) si.bPerformerTag = true; }

          if (ti.AlbumTitle != null) { si.Album = ti.AlbumTitle.Trim(); if (!string.IsNullOrEmpty(si.Album)) si.bAlbumTag = true; }
        }
        catch { si.bException = true; } // Threw an exception
      }
      else using (TagLib.File tagFile = TagLib.File.Create(file)) // Use taglib-sharp...
      {
        try
        {
          si.FileType = SongInfo2.FT_MP3; // mp3 tag info

          if (tagFile == null)
          {
            si.bException = true; // Error
            return si;
          }

          si.Duration = tagFile.Properties.Duration.ToString(); if (!string.IsNullOrEmpty(si.Duration)) si.bDurationTag = true;

          // Try to read the v2 tag-frame...
          TagLib.Id3v2.Tag tagv2 = (TagLib.Id3v2.Tag)tagFile.GetTag(TagLib.TagTypes.Id3v2);

          if (tagv2 != null)
          {
            if (tagv2.Title != null) { si.Title = tagv2.Title.Trim(); if (!string.IsNullOrEmpty(si.Title)) si.bTitleTag = true; }
            if (tagv2.FirstAlbumArtist != null) { si.Artist = tagv2.FirstAlbumArtist.Trim(); if (!string.IsNullOrEmpty(si.Artist)) si.bArtistTag = true; }
            if (tagv2.FirstPerformer != null) { si.Performer = tagv2.FirstPerformer.Trim(); if (!string.IsNullOrEmpty(si.Performer)) si.bPerformerTag = true; }  // Same as Author in a wma file
            if (tagv2.Album != null) { si.Album = tagv2.Album.Trim(); if (!string.IsNullOrEmpty(si.Album)) si.bAlbumTag = true; }
          }
          else // Try to read the old v1 tag
          {
            // Try to read the v1 tag-frame...
            TagLib.Id3v1.Tag tagv1 = (TagLib.Id3v1.Tag)tagFile.GetTag(TagLib.TagTypes.Id3v1);

            if (tagv1.Title != null) { si.Title = tagv1.Title.Trim(); if (!string.IsNullOrEmpty(si.Title)) si.bTitleTag = true; }
            if (tagv1.FirstAlbumArtist != null) { si.Artist = tagv1.FirstAlbumArtist.Trim(); if (!string.IsNullOrEmpty(si.Artist)) si.bArtistTag = true; }
            if (tagv1.FirstPerformer != null) { si.Performer = tagv1.FirstPerformer.Trim(); if (!string.IsNullOrEmpty(si.Performer)) si.bPerformerTag = true; }  // Same as Author in a wma file
            if (tagv1.Album != null) { si.Album = tagv1.Album.Trim(); if (!string.IsNullOrEmpty(si.Album)) si.bAlbumTag = true; }
          }
        }
        catch { si.bException = true; } // Threw an exception
      }

      return si;
    }
    //---------------------------------------------------------------------------
    /// <summary>
    /// Delete all media tags
    /// </summary>
    /// <param name="file">Media file name</param>
    /// <returns># deleted or a negative # if error</returns>
    public int DeleteAll(string file)
    {
      string ext = Path.GetExtension(file).ToLower();

      if (ext == ".wma" || ext == ".asf" || ext == ".wmv" || ext == ".wm")
      {
        MediaDataManager mdm = new MediaDataManager();

        if (mdm == null) return -10;

        try { return mdm.DeleteAllAttrib(file); }  // use -1 for the stream (all, I'm presuming...)
        catch { return -11; }
      }

      using (TagLib.File tagFile = TagLib.File.Create(file)) // Use taglib-sharp...
      {
        if (tagFile == null) return -12;

        try
        {
          if (tagFile.Writeable)
          {
            tagFile.RemoveTags(TagTypes.AllTags);
            tagFile.Save();
            return 0; // Where is # deleted??? 0 means "no error" but count unknown :-)
          }
          else return -13;
        }
        catch { return -14; }
      }
    }
    //---------------------------------------------------------------------------
    public SongInfo2 ParsePath(string path)
    {
      SongInfo2 si = new SongInfo2();
      SongInfo2.init(si);

      try
      {
        si.Title = Path.GetFileNameWithoutExtension(path); // Title

        int comparelen = 3; // expected tokins: artist, album, song

        // If MediaTags g_root property was set, parse it out of the song-path
        if (!string.IsNullOrEmpty(g_root) && path.StartsWith(g_root)) path = path.Substring(g_root.Length, path.Length-g_root.Length);

        if (Path.IsPathRooted(path)) comparelen++; // Adjust # expected tokins up if drive-letter present...

        // Split into 0=drive, 1=artist, 2=album, 3=song
        // Split into 0=artist, 1=album, 2=song (if not rooted)
        string[] paths = path.Split(Path.DirectorySeparatorChar);

        int len = paths.Length;

        if (len >= comparelen)
        {
          si.Artist = paths[len - 3]; // Artist
          si.Album = paths[len - 2]; // Album
        }
        else if (len >= comparelen - 1) si.Album = paths[len - 2]; // Album
      }
      catch { si.bException = true; } // Threw an exception

      return si;
    }
    //---------------------------------------------------------------------------
    public List<KeyValuePair<string, string>> GetSongAttributesWMA(string filePath)
    //
    // Returns a list of KeyValuePair objects with name:value ofsong attributes
    //
    // Example: WM/AlbumArtist:Scott Swift
    {
      List<KeyValuePair<string, string>> sl = new List<KeyValuePair<string, string>>();
      MetadataEditor me = new MetadataEditor(filePath);

      foreach (WinMediaLib.Attribute a in me) sl.Add(new KeyValuePair<string, string>(a.Name, a.Value.ToString()));

      return sl;
    }
    //---------------------------------------------------------------------------
    public void SetValueWMA(string filePath, string attrName, ushort attrType, string newValue)
    //
    // attrName is "WM/AlbumArtist", or "Author", "Title", "WM/Genre" etc
    //
    // attrType:
    // WM_TYPE_DWORD = 0;
    // WM_TYPE_STRING = 1;
    // WM_TYPE_BINARY = 2;
    // WM_TYPE_BOOL = 3;
    // WM_TYPE_QWORD = 4;
    // WM_TYPE_WORD = 5;
    // WM_TYPE_GUID = 6;
    //
    // newValue is always passed as a string but is converted to the specified type later!
    {
      try
      {
        MediaDataManager mdm = new MediaDataManager();

        // fileName, stream # (0), attr name, attr type (0-6), attr new value
        mdm.SetAttrib(filePath, 0, attrName, attrType, newValue);
      }
      catch { }
    }

    // Windows Media (.wma etc.)
    //**********************************************************************
    //
    //* Idx  Name                   Stream Language Type  Value
    //* ---  ----                   ------ -------- ----  -----
    //*   0  Duration                    0    0    QWORD  2727330000
    //*   1  Bitrate                     0    0    DWORD  772947
    //*   2  Seekable                    0    0     BOOL  True
    //*   3  Stridable                   0    0     BOOL  False
    //*   4  Broadcast                   0    0     BOOL  False
    //*   5  Is_Protected                0    0     BOOL  False
    //*   6  Is_Trusted                  0    0     BOOL  False
    //*   7  Signature_Name              0    0   STRING  ""
    //*   8  HasAudio                    0    0     BOOL  True
    //*   9  HasImage                    0    0     BOOL  False
    //*  10  HasScript                   0    0     BOOL  False
    //*  11  HasVideo                    0    0     BOOL  False
    //*  12  CurrentBitrate              0    0    DWORD  745417
    //*  13  OptimalBitrate              0    0    DWORD  745417
    //*  14  HasAttachedImages           0    0     BOOL  False
    //*  15  Can_Skip_Backward           0    0     BOOL  False
    //*  16  Can_Skip_Forward            0    0     BOOL  False
    //*  17  FileSize                    0    0    QWORD  25475943
    //*  18  HasArbitraryDataStream      0    0     BOOL  False
    //*  19  HasFileTransferStream       0    0     BOOL  False
    //*  20  WM/ContainerFormat          0    0    DWORD  1
    //*  21  Title                       0    0   STRING  "New Title"
    //*  22  Author                      0    0   STRING  "Arrested Development"
    //*  23  WM/Lyrics                   0    0   STRING  ""
    //*  24  WM/MediaPrimaryClassID      0    0   STRING  "{D1607DBC-E323-4BE2-86A1-48A42A28441E}"
    //*  25  WMFSDKVersion               0    0   STRING  "9.00.00.3250"
    //*  26  WMFSDKNeeded                0    0   STRING  "0.0.0.0000"
    //*  27  IsVBR                       0    0     BOOL  True
    //*  28  ASFLeakyBucketPairs         0    0   BINARY  [114 bytes]
    //*  29  WM/Year                     0    0   STRING  "1998"
    //*  30  WM/EncodingTime             0    0    QWORD  127529672390000000
    //*  31  WM/UniqueFileIdentifier     0    0   STRING  "AMGa_id=R   354431;AMGp_id=P        6;AMGt_id=T  2168780"
    //*  32  WM/Composer                 0    0   STRING  "Arrested Development"
    //*  33  WM/Publisher                0    0   STRING  "EMI"
    //*  34  WM/Genre                    0    0   STRING  "Rap & Hip Hop"
    //*  35  WM/AlbumTitle               0    0   STRING  "The Best of Arrested Development"
    //*  36  WM/AlbumArtist              0    0   STRING  "Arrested Development"
    //*  37  WM/MCDI                     0    0   BINARY  [124 bytes]
    //*  38  WM/Provider                 0    0   STRING  "AMG"
    //*  39  WM/ProviderRating           0    0   STRING  "7"
    //*  40  WM/ProviderStyle            0    0   STRING  "Rap"
    //*  41  WM/TrackNumber              0    0    DWORD  4
    //*  42  WM/MediaClassPrimaryID      0    0     GUID  BC-7D-60-D1-23-E3-E2-4B-86-A1-48-A4-2A-28-44-1E
    //*  43  WM/MediaClassSecondaryID    0    0     GUID  00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00
    //*  44  WM/WMContentID              0    0     GUID  89-37-4F-D5-5D-58-C9-49-B5-89-EE-26-64-D3-5F-4F
    //*  45  WM/Composer                 0    0   STRING  "Speech"
    //*  46  WM/WMCollectionID           0    0     GUID  9C-1E-F2-00-D9-73-CE-42-83-B1-44-FB-82-CE-3A-0F
    //*  47  WM/WMCollectionGroupID      0    0     GUID  9C-1E-F2-00-D9-73-CE-42-83-B1-44-FB-82-CE-3A-0F
    //*  48  WM/ASFPacketCount           0    0    QWORD  1900
    //*  49  WM/ASFSecurityObjectsSize   0    0    QWORD  0
    //*  50  WM/Text                     0    0   STRING  ""
    //*  51  WM/Composer                 0    0   STRING  ""
    //*  52  WM/Conductor                0    0   STRING  ""
    //**********************************************************************
    // Added this 12/2014
    // ASFLeakyBucketPairs	g_wszASFLeakyBucketPairs	WMT_TYPE_BINARY
    // AspectRatioX	g_wszWMAspectRatioX	WMT_TYPE_DWORD
    // AspectRatioY	g_wszWMAspectRatioY	WMT_TYPE_DWORD
    // Author	g_wszWMAuthor	WMT_TYPE_STRING
    // AverageLevel	g_wszAverageLevel	WMT_TYPE_DWORD
    // BannerImageData	g_wszWMBannerImageData	WMT_TYPE_BINARY
    // BannerImageType	g_wszWMBannerImageType	WMT_TYPE_DWORD
    // BannerImageURL	g_wszWMBannerImageURL	WMT_TYPE_STRING
    // Bitrate	g_wszWMBitrate	WMT_TYPE_DWORD
    // Broadcast	g_wszWMBroadcast	WMT_TYPE_BOOL
    // BufferAverage	g_wszBufferAverage	WMT_TYPE_DWORD
    // Can_Skip_Backward	g_wszWMSkipBackward	WMT_TYPE_BOOL
    // Can_Skip_Forward	g_wszWMSkipForward	WMT_TYPE_BOOL
    // Copyright	g_wszWMCopyright	WMT_TYPE_STRING
    // CopyrightURL	g_wszWMCopyrightURL	WMT_TYPE_STRING
    // CurrentBitrate	g_wszWMCurrentBitrate	WMT_TYPE_DWORD
    // Description	g_wszWMDescription	WMT_TYPE_STRING
    // DRM_ContentID	g_wszWMDRM_ContentID	WMT_TYPE_STRING
    // DRM_DRMHeader_ContentDistributor	g_wszWMDRM_DRMHeader_ContentDistributor	WMT_TYPE_STRING
    // DRM_DRMHeader_ContentID	g_wszWMDRM_DRMHeader_ContentID	WMT_TYPE_STRING
    // DRM_DRMHeader_IndividualizedVersion	g_wszWMDRM_DRMHeader_IndividualizedVersion	WMT_TYPE_STRING
    // DRM_DRMHeader_KeyID	g_wszWMDRM_DRMHeader_KeyID	WMT_TYPE_STRING
    // DRM_DRMHeader_LicenseAcqURL	g_wszWMDRM_DRMHeader_LicenseAcqURL	WMT_TYPE_STRING
    // DRM_DRMHeader_SubscriptionContentID	g_wszWMDRM_DRMHeader_SubscriptionContentID	WMT_TYPE_STRING
    // DRM_DRMHeader	g_wszWMDRM_DRMHeader	WMT_TYPE_STRING
    // DRM_IndividualizedVersion	g_wszWMDRM_IndividualizedVersion	WMT_TYPE_STRING
    // DRM_KeyID	g_wszWMDRM_KeyID	WMT_TYPE_STRING
    // DRM_LASignatureCert	g_wszWMDRM_LASignatureCert	WMT_TYPE_STRING
    // DRM_LASignatureLicSrvCert	g_wszWMDRM_LASignatureLicSrvCert	WMT_TYPE_STRING
    // DRM_LASignaturePrivKey	g_wszWMDRM_LASignaturePrivKey	WMT_TYPE_STRING
    // DRM_LASignatureRootCert	g_wszWMDRM_LASignatureRootCert	WMT_TYPE_STRING
    // DRM_LicenseAcqURL	g_wszWMDRM_LicenseAcqURL	WMT_TYPE_STRING
    // DRM_LicenseID	g_wszWMDRM_LicenseID	WMT_TYPE_STRING
    // DRM_SourceID	g_wszWMDRM_SourceID	WMT_TYPE_DWORD
    // DRM_V1LicenseAcqURL	g_wszWMDRM_V1LicenseAcqURL	WMT_TYPE_STRING
    // Duration	g_wszWMDuration	WMT_TYPE_QWORD
    // FileSize	g_wszWMFileSize	WMT_TYPE_QWORD
    // HasArbitraryDataStream	g_wszWMHasArbitraryDataStream	WMT_TYPE_BOOL
    // HasAttachedImages	g_wszWMHasAttachedImages	WMT_TYPE_BOOL
    // HasAudio	g_wszWMHasAudio	WMT_TYPE_BOOL
    // HasFileTransferStream	g_wszWMHasFileTransferStream	WMT_TYPE_BOOL
    // HasImage	g_wszWMHasImage	WMT_TYPE_BOOL
    // HasScript	g_wszWMHasScript	WMT_TYPE_BOOL
    // HasVideo	g_wszWMHasVideo	WMT_TYPE_BOOL
    // Is_Protected	g_wszWMProtected	WMT_TYPE_BOOL
    // Is_Trusted	g_wszWMTrusted	WMT_TYPE_BOOL
    // ISAN	g_wszISAN	WMT_TYPE_STRING
    // IsVBR	g_wszWMIsVBR	WMT_TYPE_BOOL
    // NSC_Address	g_wszWMNSCAddress	WMT_TYPE_STRING
    // NSC_Description	g_wszWMNSCDescription	WMT_TYPE_STRING
    // NSC_Email	g_wszWMNSCEmail	WMT_TYPE_STRING
    // NSC_Name	g_wszWMNSCName	WMT_TYPE_STRING
    // NSC_Phone	g_wszWMNSCPhone	WMT_TYPE_STRING
    // NumberOfFrames	g_wszWMNumberOfFrames	WMT_TYPE_QWORD
    // OptimalBitrate	g_wszWMOptimalBitrate	WMT_TYPE_DWORD
    // PeakValue	g_wszPeakValue	WMT_TYPE_DWORD
    // Rating	g_wszWMRating	WMT_TYPE_STRING
    // Seekable	g_wszWMSeekable	WMT_TYPE_BOOL
    // Signature_Name	g_wszWMSignature_Name	WMT_TYPE_STRING
    // Stridable	g_wszWMStridable	WMT_TYPE_BOOL
    // Title	g_wszWMTitle	WMT_TYPE_STRING
    // VBRPeak	g_wszVBRPeak	WMT_TYPE_DWORD
    // WM/AlbumArtist	g_wszWMAlbumArtist	WMT_TYPE_STRING
    // WM/AlbumCoverURL	g_wszWMAlbumCoverURL	WMT_TYPE_STRING
    // WM/AlbumTitle	g_wszWMAlbumTitle	WMT_TYPE_STRING
    // WM/ASFPacketCount	g_wszWMASFPacketCount	WMT_TYPE_QWORD
    // WM/ASFSecurityObjectsSize	g_wszWMASFSecurityObjectsSize	WMT_TYPE_QWORD
    // WM/AudioFileURL	g_wszWMAudioFileURL	WMT_TYPE_STRING
    // WM/AudioSourceURL	g_wszWMAudioSourceURL	WMT_TYPE_STRING
    // WM/AuthorURL	g_wszWMAuthorURL	WMT_TYPE_STRING
    // WM/BeatsPerMinute	g_wszWMBeatsPerMinute	WMT_TYPE_STRING
    // WM/Category	g_wszWMCategory	WMT_TYPE_STRING
    // WM/Codec	g_wszWMCodec	WMT_TYPE_STRING
    // WM/Composer	g_wszWMComposer	WMT_TYPE_STRING
    // WM/Conductor	g_wszWMConductor	WMT_TYPE_STRING
    // WM/ContainerFormat	g_wszWMContainerFormat	WMT_STORAGE_FORMAT (WMT_TYPE_BINARY)
    // WM/ContentDistributor	g_wszWMContentDistributor	WMT_TYPE_STRING
    // WM/ContentGroupDescription	g_wszWMContentGroupDescription	WMT_TYPE_STRING
    // WM/Director	g_wszWMDirector	WMT_TYPE_STRING
    // WM/DRM	g_wszWMDRM	WMT_TYPE_STRING
    // WM/DVDID	g_wszWMDVDID	WMT_TYPE_STRING
    // WM/EncodedBy	g_wszWMEncodedBy	WMT_TYPE_STRING
    // WM/EncodingSettings	g_wszWMEncodingSettings	WMT_TYPE_STRING
    // WM/EncodingTime	g_wszWMEncodingTime	FILETIME (WMT_TYPE_QWORD)
    // WM/Genre	g_wszWMGenre	WMT_TYPE_STRING
    // WM/GenreID	g_wszWMGenreID	WMT_TYPE_STRING
    // WM/InitialKey	g_wszWMInitialKey	WMT_TYPE_STRING
    // WM/ISRC	g_wszWMISRC	WMT_TYPE_STRING
    // WM/Language	g_wszWMLanguage	WMT_TYPE_STRING
    // WM/Lyrics	g_wszWMLyrics	WMT_TYPE_STRING
    // WM/Lyrics_Synchronised	g_wszWMLyrics_Synchronised	WM_SYNCHRONISED_LYRICS (WMT_TYPE_BINARY)
    // WM/MCDI	g_wszWMMCDI	WMT_TYPE_BINARY
    // WM/MediaClassPrimaryID	g_wszWMMediaClassPrimaryID	WMT_TYPE_GUID
    // WM/MediaClassSecondaryID	g_wszWMMediaClassSecondaryID	WMT_TYPE_GUID
    // WM/MediaCredits	g_wszWMMediaCredits	WMT_TYPE_STRING
    // WM/MediaIsDelay	g_wszWMMediaIsDelay	WMT_TYPE_BOOL
    // WM/MediaIsFinale	g_wszWMMediaIsFinale	WMT_TYPE_BOOL
    // WM/MediaIsLive	g_wszWMMediaIsLive	WMT_TYPE_BOOL
    // WM/MediaIsPremiere	g_wszWMMediaIsPremiere	WMT_TYPE_BOOL
    // WM/MediaIsRepeat	g_wszWMMediaIsRepeat	WMT_TYPE_BOOL
    // WM/MediaIsSAP	g_wszWMMediaIsSAP	WMT_TYPE_BOOL
    // WM/MediaIsStereo	g_wszWMMediaIsStereo	WMT_TYPE_BOOL
    // WM/MediaIsSubtitled	g_wszWMMediaIsSubtitled	WMT_TYPE_BOOL
    // WM/MediaIsTape	g_wszWMMediaIsTape	WMT_TYPE_BOOL
    // WM/MediaNetworkAffiliation	g_wszWMMediaNetworkAffiliation	WMT_TYPE_STRING
    // WM/MediaOriginalBroadcastDateTime	g_wszWMMediaOriginalBroadcastDateTime	WMT_TYPE_STRING
    // WM/MediaOriginalChannel	g_wszWMMediaOriginalChannel	WMT_TYPE_STRING
    // WM/MediaStationCallSign	g_wszWMMediaStationCallSign	WMT_TYPE_STRING
    // WM/MediaStationName	g_wszWMMediaStationName	WMT_TYPE_STRING
    // WM/ModifiedBy	g_wszWMModifiedBy	WMT_TYPE_STRING
    // WM/Mood	g_wszWMMood	WMT_TYPE_STRING
    // WM/OriginalAlbumTitle	g_wszWMOriginalAlbumTitle	WMT_TYPE_STRING
    // WM/OriginalArtist	g_wszWMOriginalArtist	WMT_TYPE_STRING
    // WM/OriginalFilename	g_wszWMOriginalFilename	WMT_TYPE_STRING
    // WM/OriginalLyricist	g_wszWMOriginalLyricist	WMT_TYPE_STRING
    // WM/OriginalReleaseTime	g_wszWMOriginalReleaseTime	WMT_TYPE_STRING
    // WM/OriginalReleaseYear	g_wszWMOriginalReleaseYear	WMT_TYPE_STRING
    // WM/ParentalRating	g_wszWMParentalRating	WMT_TYPE_STRING
    // WM/ParentalRatingReason	g_wszWMParentalRatingReason	WMT_TYPE_STRING
    // WM/PartOfSet	g_wszWMPartOfSet	WMT_TYPE_STRING
    // WM/PeakBitrate	g_wszWMPeakBitrate	WMT_TYPE_DWORD
    // WM/Period	g_wszWMPeriod	WMT_TYPE_STRING
    // WM/Picture	g_wszWMPicture	WM_PICTURE (WMT_TYPE_BINARY)
    // WM/PlaylistDelay	g_wszWMPlaylistDelay	WMT_TYPE_STRING
    // WM/Producer	g_wszWMProducer	WMT_TYPE_STRING
    // WM/PromotionURL	g_wszWMPromotionURL	WMT_TYPE_STRING
    // WM/ProtectionType	g_wszWMProtectionType	WMT_TYPE_STRING
    // WM/Provider	g_wszWMProvider	WMT_TYPE_STRING
    // WM/ProviderCopyright	g_wszWMProviderCopyright	WMT_TYPE_STRING
    // WM/ProviderRating	g_wszWMProviderRating	WMT_TYPE_STRING
    // WM/ProviderStyle	g_wszWMProviderStyle	WMT_TYPE_STRING
    // WM/Publisher	g_wszWMPublisher	WMT_TYPE_STRING
    // WM/RadioStationName	g_wszWMRadioStationName	WMT_TYPE_STRING
    // WM/RadioStationOwner	g_wszWMRadioStationOwner	WMT_TYPE_STRING
    // WM/SharedUserRating	g_wszWMSharedUserRating	WMT_TYPE_DWORD
    // WM/StreamTypeInfo	g_wszWMStreamTypeInfo	WM_STREAM_TYPE_INFO (WMT_TYPE_BINARY)
    // WM/SubscriptionContentID	g_wszWMSubscriptionContentID	WMT_TYPE_STRING
    // WM/SubTitle	g_wszWMSubTitle	WMT_TYPE_STRING
    // WM/SubTitleDescription	g_wszWMSubTitleDescription	WMT_TYPE_STRING
    // WM/Text	g_wszWMText	WM_USER_TEXT (WMT_TYPE_BINARY)
    // WM/ToolName	g_wszWMToolName	WMT_TYPE_STRING
    // WM/ToolVersion	g_wszWMToolVersion	WMT_TYPE_STRING
    // WM/Track	g_wszWMTrack	WMT_TYPE_STRING
    // WM/TrackNumber	g_wszWMTrackNumber	WMT_TYPE_STRING
    // WM/UniqueFileIdentifier	g_wszWMUniqueFileIdentifier	WMT_TYPE_STRING
    // WM/UserWebURL	g_wszWMUserWebURL	WM_USER_WEB_URL (WMT_TYPE_BINARY)
    // WM/VideoClosedCaptioning	g_wszWMVideoClosedCaptioning	WMT_TYPE_BOOL
    // WM/VideoFrameRate	g_wszWMVideoFrameRate	WMT_TYPE_DWORD
    // WM/VideoHeight	g_wszWMVideoHeight	WMT_TYPE_DWORD
    // WM/VideoWidth	g_wszWMVideoWidth	WMT_TYPE_DWORD
    // WM/WMADRCAverageReference	g_wszWMWMADRCAverageReference	WMT_TYPE_DWORD
    // WM/WMADRCAverageTarget	g_wszWMWMADRCAverageTarget	WMT_TYPE_DWORD
    // WM/WMADRCPeakReference	g_wszWMWMADRCPeakReference	WMT_TYPE_DWORD
    // WM/WMADRCPeakTarget	g_wszWMWMADRCPeakTarget	WMT_TYPE_DWORD
    // WM/WMCollectionGroupID	g_wszWMWMCollectionGroupID	WMT_TYPE_GUID
    // WM/WMCollectionID	g_wszWMWMCollectionID	WMT_TYPE_GUID
    // WM/WMContentID	g_wszWMWMContentID	WMT_TYPE_GUID
    // WM/WMShadowFileSourceDRMType	g_wszWMWMShadowFileSourceDRMType	WMT_TYPE_STRING
    // WM/WMShadowFileSourceFileType	g_wszWMWMShadowFileSourceFileType	WMT_TYPE_STRING
    // WM/Writer	g_wszWMWriter	WMT_TYPE_STRING
    // WM/Year	g_wszWMYear	WMT_TYPE_STRING
  }
}
