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
    internal string acoustId;
    internal string mbId;
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

    public string AcoustID
    { get { return acoustId; } }

    public string MBID
    { get { return mbId; } }

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
//*  24  AcoustID                    0    0   STRING  ""
//*  25  MBID                        0    0   STRING  ""
//*  26  WM/MediaPrimaryClassID      0    0   STRING  "{D1607DBC-E323-4BE2-86A1-48A42A28441E}"
//*  27  WMFSDKVersion               0    0   STRING  "9.00.00.3250"
//*  28  WMFSDKNeeded                0    0   STRING  "0.0.0.0000"
//*  29  IsVBR                       0    0     BOOL  True
//*  30  ASFLeakyBucketPairs         0    0   BINARY  [114 bytes]
//*  31  WM/Year                     0    0   STRING  "1998"
//*  32  WM/EncodingTime             0    0    QWORD  127529672390000000
//*  33  WM/UniqueFileIdentifier     0    0   STRING  "AMGa_id=R   354431;AMGp_id=P        6;AMGt_id=T  2168780"
//*  34  WM/Composer                 0    0   STRING  "Arrested Development"
//*  35  WM/Publisher                0    0   STRING  "EMI"
//*  36  WM/Genre                    0    0   STRING  "Rap & Hip Hop"
//*  37  WM/AlbumTitle               0    0   STRING  "The Best of Arrested Development"
//*  38  WM/AlbumArtist              0    0   STRING  "Arrested Development"
//*  39  WM/MCDI                     0    0   BINARY  [124 bytes]
//*  40  WM/Provider                 0    0   STRING  "AMG"
//*  41  WM/ProviderRating           0    0   STRING  "7"
//*  42  WM/ProviderStyle            0    0   STRING  "Rap"
//*  43  WM/TrackNumber              0    0    DWORD  4
//*  44  WM/MediaClassPrimaryID      0    0     GUID  BC-7D-60-D1-23-E3-E2-4B-86-A1-48-A4-2A-28-44-1E
//*  45  WM/MediaClassSecondaryID    0    0     GUID  00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00
//*  46  WM/WMContentID              0    0     GUID  89-37-4F-D5-5D-58-C9-49-B5-89-EE-26-64-D3-5F-4F
//*  47  WM/Composer                 0    0   STRING  "Speech"
//*  48  WM/WMCollectionID           0    0     GUID  9C-1E-F2-00-D9-73-CE-42-83-B1-44-FB-82-CE-3A-0F
//*  49  WM/WMCollectionGroupID      0    0     GUID  9C-1E-F2-00-D9-73-CE-42-83-B1-44-FB-82-CE-3A-0F
//*  50  WM/ASFPacketCount           0    0    QWORD  1900
//*  51  WM/ASFSecurityObjectsSize   0    0    QWORD  0
//*  52  WM/Text                     0    0   STRING  ""
//*  53  WM/Composer                 0    0   STRING  ""
//*  54  WM/Conductor                0    0   STRING  ""
//*********************************************************************
