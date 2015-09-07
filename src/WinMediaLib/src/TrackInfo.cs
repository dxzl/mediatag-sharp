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
	public struct TrackInfo
	{
		internal string title;
		internal string author;
		internal string albumTitle;
    internal string albumArtist;
    internal string publisher;
		internal string genre;
    internal string lyrics;
    internal string text;
    internal string composer;
    internal string conductor;
    internal string year;
    internal uint track;
    internal TimeSpan duration;
		internal uint bitRate;
		internal bool isProtected;
		internal ulong fileSize;

		public string Title 
			{ get { return title; } }

		public string Author 
			{ get { return author; } }

		public string AlbumTitle 
			{ get { return albumTitle; } }

    public string AlbumArtist
    { get { return albumArtist; } }

    public string Publisher 
			{ get { return publisher; } }

		public string Genre 
			{ get { return genre; } }

    public string Lyrics
    { get { return lyrics; } }

    public string Text
    { get { return text; } }

    public string Composer
    { get { return composer; } }

    public string Conductor
    { get { return conductor; } }

    public string Year
    { get { return year; } }

    public uint Track
    { get { return track; } }

    public TimeSpan Duration 
			{ get { return duration; } }

		public uint BitRate 
			{ get { return bitRate; } }

		public bool IsProtected
			{ get { return isProtected; } }

		public ulong FileSize 
			{ get { return fileSize; } }	
	}
}

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
