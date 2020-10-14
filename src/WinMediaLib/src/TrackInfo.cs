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
// https://docs.microsoft.com/en-us/windows/win32/wmformat/attribute-list
//Attribute name	Global identifier	Data type
//---------------------------------------------------------------------
//ASFLeakyBucketPairs	g_wszASFLeakyBucketPairs	WMT_TYPE_BINARY
//AspectRatioX	g_wszWMAspectRatioX	WMT_TYPE_DWORD
//AspectRatioY	g_wszWMAspectRatioY	WMT_TYPE_DWORD
//Author	g_wszWMAuthor	WMT_TYPE_STRING
//AverageLevel	g_wszAverageLevel	WMT_TYPE_DWORD
//BannerImageData	g_wszWMBannerImageData	WMT_TYPE_BINARY
//BannerImageType	g_wszWMBannerImageType	WMT_TYPE_DWORD
//BannerImageURL	g_wszWMBannerImageURL	WMT_TYPE_STRING
//Bitrate	g_wszWMBitrate	WMT_TYPE_DWORD
//Broadcast	g_wszWMBroadcast	WMT_TYPE_BOOL
//BufferAverage	g_wszBufferAverage	WMT_TYPE_DWORD
//Can_Skip_Backward	g_wszWMSkipBackward	WMT_TYPE_BOOL
//Can_Skip_Forward	g_wszWMSkipForward	WMT_TYPE_BOOL
//Copyright	g_wszWMCopyright	WMT_TYPE_STRING
//CopyrightURL	g_wszWMCopyrightURL	WMT_TYPE_STRING
//CurrentBitrate	g_wszWMCurrentBitrate	WMT_TYPE_DWORD
//Description	g_wszWMDescription	WMT_TYPE_STRING
//DRM_ContentID	g_wszWMDRM_ContentID	WMT_TYPE_STRING
//DRM_DRMHeader_ContentDistributor	g_wszWMDRM_DRMHeader_ContentDistributor	WMT_TYPE_STRING
//DRM_DRMHeader_ContentID	g_wszWMDRM_DRMHeader_ContentID	WMT_TYPE_STRING
//DRM_DRMHeader_IndividualizedVersion	g_wszWMDRM_DRMHeader_IndividualizedVersion	WMT_TYPE_STRING
//DRM_DRMHeader_KeyID	g_wszWMDRM_DRMHeader_KeyID	WMT_TYPE_STRING
//DRM_DRMHeader_LicenseAcqURL	g_wszWMDRM_DRMHeader_LicenseAcqURL	WMT_TYPE_STRING
//DRM_DRMHeader_SubscriptionContentID	g_wszWMDRM_DRMHeader_SubscriptionContentID	WMT_TYPE_STRING
//DRM_DRMHeader	g_wszWMDRM_DRMHeader	WMT_TYPE_STRING
//DRM_IndividualizedVersion	g_wszWMDRM_IndividualizedVersion	WMT_TYPE_STRING
//DRM_KeyID	g_wszWMDRM_KeyID	WMT_TYPE_STRING
//DRM_LASignatureCert	g_wszWMDRM_LASignatureCert	WMT_TYPE_STRING
//DRM_LASignatureLicSrvCert	g_wszWMDRM_LASignatureLicSrvCert	WMT_TYPE_STRING
//DRM_LASignaturePrivKey	g_wszWMDRM_LASignaturePrivKey	WMT_TYPE_STRING
//DRM_LASignatureRootCert	g_wszWMDRM_LASignatureRootCert	WMT_TYPE_STRING
//DRM_LicenseAcqURL	g_wszWMDRM_LicenseAcqURL	WMT_TYPE_STRING
//DRM_LicenseID	g_wszWMDRM_LicenseID	WMT_TYPE_STRING
//DRM_SourceID	g_wszWMDRM_SourceID	WMT_TYPE_DWORD
//DRM_V1LicenseAcqURL	g_wszWMDRM_V1LicenseAcqURL	WMT_TYPE_STRING
//Duration	g_wszWMDuration	WMT_TYPE_QWORD
//FileSize	g_wszWMFileSize	WMT_TYPE_QWORD
//HasArbitraryDataStream	g_wszWMHasArbitraryDataStream	WMT_TYPE_BOOL
//HasAttachedImages	g_wszWMHasAttachedImages	WMT_TYPE_BOOL
//HasAudio	g_wszWMHasAudio	WMT_TYPE_BOOL
//HasFileTransferStream	g_wszWMHasFileTransferStream	WMT_TYPE_BOOL
//HasImage	g_wszWMHasImage	WMT_TYPE_BOOL
//HasScript	g_wszWMHasScript	WMT_TYPE_BOOL
//HasVideo	g_wszWMHasVideo	WMT_TYPE_BOOL
//Is_Protected	g_wszWMProtected	WMT_TYPE_BOOL
//Is_Trusted	g_wszWMTrusted	WMT_TYPE_BOOL
//ISAN	g_wszISAN	WMT_TYPE_STRING
//IsVBR	g_wszWMIsVBR	WMT_TYPE_BOOL
//NSC_Address	g_wszWMNSCAddress	WMT_TYPE_STRING
//NSC_Description	g_wszWMNSCDescription	WMT_TYPE_STRING
//NSC_Email	g_wszWMNSCEmail	WMT_TYPE_STRING
//NSC_Name	g_wszWMNSCName	WMT_TYPE_STRING
//NSC_Phone	g_wszWMNSCPhone	WMT_TYPE_STRING
//NumberOfFrames	g_wszWMNumberOfFrames	WMT_TYPE_QWORD
//OptimalBitrate	g_wszWMOptimalBitrate	WMT_TYPE_DWORD
//PeakValue	g_wszPeakValue	WMT_TYPE_DWORD
//Rating	g_wszWMRating	WMT_TYPE_STRING
//Seekable	g_wszWMSeekable	WMT_TYPE_BOOL
//Signature_Name	g_wszWMSignature_Name	WMT_TYPE_STRING
//Stridable	g_wszWMStridable	WMT_TYPE_BOOL
//Title	g_wszWMTitle	WMT_TYPE_STRING
//VBRPeak	g_wszVBRPeak	WMT_TYPE_DWORD
//WM/AlbumArtist	g_wszWMAlbumArtist	WMT_TYPE_STRING
//WM/AlbumCoverURL	g_wszWMAlbumCoverURL	WMT_TYPE_STRING
//WM/AlbumTitle	g_wszWMAlbumTitle	WMT_TYPE_STRING
//WM/ASFPacketCount	g_wszWMASFPacketCount	WMT_TYPE_QWORD
//WM/ASFSecurityObjectsSize	g_wszWMASFSecurityObjectsSize	WMT_TYPE_QWORD
//WM/AudioFileURL	g_wszWMAudioFileURL	WMT_TYPE_STRING
//WM/AudioSourceURL	g_wszWMAudioSourceURL	WMT_TYPE_STRING
//WM/AuthorURL	g_wszWMAuthorURL	WMT_TYPE_STRING
//WM/BeatsPerMinute	g_wszWMBeatsPerMinute	WMT_TYPE_STRING
//WM/Category	g_wszWMCategory	WMT_TYPE_STRING
//WM/Codec	g_wszWMCodec	WMT_TYPE_STRING
//WM/Composer	g_wszWMComposer	WMT_TYPE_STRING
//WM/Conductor	g_wszWMConductor	WMT_TYPE_STRING
//WM/ContainerFormat	g_wszWMContainerFormat	WMT_STORAGE_FORMAT (WMT_TYPE_BINARY)
//WM/ContentDistributor   g_wszWMContentDistributor WMT_TYPE_STRING
//WM/ContentGroupDescription	g_wszWMContentGroupDescription	WMT_TYPE_STRING
//WM/Director	g_wszWMDirector	WMT_TYPE_STRING
//WM/DRM	g_wszWMDRM	WMT_TYPE_STRING
//WM/DVDID	g_wszWMDVDID	WMT_TYPE_STRING
//WM/EncodedBy	g_wszWMEncodedBy	WMT_TYPE_STRING
//WM/EncodingSettings	g_wszWMEncodingSettings	WMT_TYPE_STRING
//WM/EncodingTime	g_wszWMEncodingTime	FILETIME (WMT_TYPE_QWORD)
//WM/Genre    g_wszWMGenre WMT_TYPE_STRING
//WM/GenreID	g_wszWMGenreID	WMT_TYPE_STRING
//WM/InitialKey	g_wszWMInitialKey	WMT_TYPE_STRING
//WM/ISRC	g_wszWMISRC	WMT_TYPE_STRING
//WM/Language	g_wszWMLanguage	WMT_TYPE_STRING
//WM/Lyrics	g_wszWMLyrics	WMT_TYPE_STRING
//WM/Lyrics_Synchronised	g_wszWMLyrics_Synchronised	WM_SYNCHRONISED_LYRICS (WMT_TYPE_BINARY)
//WM/MCDI g_wszWMMCDI WMT_TYPE_BINARY
//WM/MediaClassPrimaryID	g_wszWMMediaClassPrimaryID	WMT_TYPE_GUID
//WM/MediaClassSecondaryID	g_wszWMMediaClassSecondaryID	WMT_TYPE_GUID
//WM/MediaCredits	g_wszWMMediaCredits	WMT_TYPE_STRING
//WM/MediaIsDelay	g_wszWMMediaIsDelay	WMT_TYPE_BOOL
//WM/MediaIsFinale	g_wszWMMediaIsFinale	WMT_TYPE_BOOL
//WM/MediaIsLive	g_wszWMMediaIsLive	WMT_TYPE_BOOL
//WM/MediaIsPremiere	g_wszWMMediaIsPremiere	WMT_TYPE_BOOL
//WM/MediaIsRepeat	g_wszWMMediaIsRepeat	WMT_TYPE_BOOL
//WM/MediaIsSAP	g_wszWMMediaIsSAP	WMT_TYPE_BOOL
//WM/MediaIsStereo	g_wszWMMediaIsStereo	WMT_TYPE_BOOL
//WM/MediaIsSubtitled	g_wszWMMediaIsSubtitled	WMT_TYPE_BOOL
//WM/MediaIsTape	g_wszWMMediaIsTape	WMT_TYPE_BOOL
//WM/MediaNetworkAffiliation	g_wszWMMediaNetworkAffiliation	WMT_TYPE_STRING
//WM/MediaOriginalBroadcastDateTime	g_wszWMMediaOriginalBroadcastDateTime	WMT_TYPE_STRING
//WM/MediaOriginalChannel	g_wszWMMediaOriginalChannel	WMT_TYPE_STRING
//WM/MediaStationCallSign	g_wszWMMediaStationCallSign	WMT_TYPE_STRING
//WM/MediaStationName	g_wszWMMediaStationName	WMT_TYPE_STRING
//WM/ModifiedBy	g_wszWMModifiedBy	WMT_TYPE_STRING
//WM/Mood	g_wszWMMood	WMT_TYPE_STRING
//WM/OriginalAlbumTitle	g_wszWMOriginalAlbumTitle	WMT_TYPE_STRING
//WM/OriginalArtist	g_wszWMOriginalArtist	WMT_TYPE_STRING
//WM/OriginalFilename	g_wszWMOriginalFilename	WMT_TYPE_STRING
//WM/OriginalLyricist	g_wszWMOriginalLyricist	WMT_TYPE_STRING
//WM/OriginalReleaseTime	g_wszWMOriginalReleaseTime	WMT_TYPE_STRING
//WM/OriginalReleaseYear	g_wszWMOriginalReleaseYear	WMT_TYPE_STRING
//WM/ParentalRating	g_wszWMParentalRating	WMT_TYPE_STRING
//WM/ParentalRatingReason	g_wszWMParentalRatingReason	WMT_TYPE_STRING
//WM/PartOfSet	g_wszWMPartOfSet	WMT_TYPE_STRING
//WM/PeakBitrate	g_wszWMPeakBitrate	WMT_TYPE_DWORD
//WM/Period	g_wszWMPeriod	WMT_TYPE_STRING
//WM/Picture	g_wszWMPicture	WM_PICTURE (WMT_TYPE_BINARY)
//WM/PlaylistDelay    g_wszWMPlaylistDelay WMT_TYPE_STRING
//WM/Producer	g_wszWMProducer	WMT_TYPE_STRING
//WM/PromotionURL	g_wszWMPromotionURL	WMT_TYPE_STRING
//WM/ProtectionType	g_wszWMProtectionType	WMT_TYPE_STRING
//WM/Provider	g_wszWMProvider	WMT_TYPE_STRING
//WM/ProviderCopyright	g_wszWMProviderCopyright	WMT_TYPE_STRING
//WM/ProviderRating	g_wszWMProviderRating	WMT_TYPE_STRING
//WM/ProviderStyle	g_wszWMProviderStyle	WMT_TYPE_STRING
//WM/Publisher	g_wszWMPublisher	WMT_TYPE_STRING
//WM/RadioStationName	g_wszWMRadioStationName	WMT_TYPE_STRING
//WM/RadioStationOwner	g_wszWMRadioStationOwner	WMT_TYPE_STRING
//WM/SharedUserRating	g_wszWMSharedUserRating	WMT_TYPE_DWORD
//WM/StreamTypeInfo	g_wszWMStreamTypeInfo	WM_STREAM_TYPE_INFO (WMT_TYPE_BINARY)
//WM/SubscriptionContentID    g_wszWMSubscriptionContentID WMT_TYPE_STRING
//WM/SubTitle	g_wszWMSubTitle	WMT_TYPE_STRING
//WM/SubTitleDescription	g_wszWMSubTitleDescription	WMT_TYPE_STRING
//WM/Text	g_wszWMText	WM_USER_TEXT (WMT_TYPE_BINARY)
//WM/ToolName g_wszWMToolName WMT_TYPE_STRING
//WM/ToolVersion	g_wszWMToolVersion	WMT_TYPE_STRING
//WM/Track	g_wszWMTrack	WMT_TYPE_STRING
//WM/TrackNumber	g_wszWMTrackNumber	WMT_TYPE_STRING
//WM/UniqueFileIdentifier	g_wszWMUniqueFileIdentifier	WMT_TYPE_STRING
//WM/UserWebURL	g_wszWMUserWebURL	WM_USER_WEB_URL (WMT_TYPE_BINARY)
//WM/VideoClosedCaptioning    g_wszWMVideoClosedCaptioning WMT_TYPE_BOOL
//WM/VideoFrameRate	g_wszWMVideoFrameRate	WMT_TYPE_DWORD
//WM/VideoHeight	g_wszWMVideoHeight	WMT_TYPE_DWORD
//WM/VideoWidth	g_wszWMVideoWidth	WMT_TYPE_DWORD
//WM/WMADRCAverageReference	g_wszWMWMADRCAverageReference	WMT_TYPE_DWORD
//WM/WMADRCAverageTarget	g_wszWMWMADRCAverageTarget	WMT_TYPE_DWORD
//WM/WMADRCPeakReference	g_wszWMWMADRCPeakReference	WMT_TYPE_DWORD
//WM/WMADRCPeakTarget	g_wszWMWMADRCPeakTarget	WMT_TYPE_DWORD
//WM/WMCollectionGroupID	g_wszWMWMCollectionGroupID	WMT_TYPE_GUID
//WM/WMCollectionID	g_wszWMWMCollectionID	WMT_TYPE_GUID
//WM/WMContentID	g_wszWMWMContentID	WMT_TYPE_GUID
//WM/WMShadowFileSourceDRMType	g_wszWMWMShadowFileSourceDRMType	WMT_TYPE_STRING
//WM/WMShadowFileSourceFileType	g_wszWMWMShadowFileSourceFileType	WMT_TYPE_STRING
//WM/Writer	g_wszWMWriter	WMT_TYPE_STRING
//WM/Year	g_wszWMYear	WMT_TYPE_STRING
//Remarks
//The following constants are defined with the attributes. Each one indicates the number of a
//specific type of attribute. You do not need to use these values for anything in your applications.
//REMARKS
//Constant	Value
//g_dwWMSpecialAttributes	20
//g_dwWMContentAttributes	5
//g_dwWMNSCAttributes	5

// https://docs.microsoft.com/en-us/windows/win32/medfound/metadata-properties-for-media-files#windows-media-format-sdk-mappings
// https://docs.microsoft.com/en-us/windows/win32/wmformat/attribute-list
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
