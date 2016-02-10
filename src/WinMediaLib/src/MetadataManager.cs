using System;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace WinMediaLib
{
	/// <summary>
	/// Acts as a high-level API for retrieving information from one or more 
	/// media files. You can use this class to create a DataSet containing
	/// metadata attributes from a tree of directories accessed recursively.
	/// </summary>
	public class MediaDataManager : IDisposable
	{
		/// <summary>
		/// Used to fire TrackAdded events.
		/// </summary>
		public delegate void TrackAddedEventHandler(object sender, TrackInfoEventArgs e);

		/// <summary>
		/// An event that is triggered whenever a new track is added. Useful
		/// when you're running a long recursive operation.
		/// </summary>
		public event TrackAddedEventHandler TrackAdded;

    public void Dispose()
    {
      // We have no global resources...
    }

    public MediaDataManager()
    {
    }

		/// <summary>
		/// Retrieves structured property information for the given media file.
		/// </summary>
		/// <param name="filename">Windows Media File (.asf, .wma, .wmv file)</param>
		/// <returns>TrackInfo object containing commonly-used fields</returns>
		public TrackInfo RetrieveTrackInfo(string filename)
		{
			TrackInfo ti = new TrackInfo();
			
			using (MetadataEditor md = new MetadataEditor(filename))
			{
				ti.title = md[MediaMetadata.Title] as string;
				ti.author = md[MediaMetadata.Author] as string;
				ti.albumTitle = md[MediaMetadata.AlbumTitle] as string;
        ti.albumArtist = md[MediaMetadata.AlbumArtist] as string;
        ti.publisher = md[MediaMetadata.Publisher] as string;
        ti.genre = md[MediaMetadata.Genre] as string;
        ti.year = md[MediaMetadata.Year] as string;

        object o = md[MediaMetadata.TrackNumber];
        ti.track = (o == null ? 0 : (uint)o);

        o = md[MediaMetadata.Duration];
        ti.duration = new TimeSpan((o == null ? 0 : (long)(ulong)o));

        o = md[MediaMetadata.BitRate];
        ti.bitRate = (o == null ? 0 : (uint)o);

        o = md[MediaMetadata.IsProtected];
        ti.isProtected = (o == null ? false : (bool)o);

        o = md[MediaMetadata.FileSize];
        ti.fileSize = (o == null ? 0 : (ulong)o);

        ti.lyrics = md[MediaMetadata.Lyrics] as string;
        ti.acoustId = md[MediaMetadata.AcoustID] as string;
        ti.mbId = md[MediaMetadata.MBID] as string;
        ti.text = md[MediaMetadata.Text] as string;
        ti.composer = md[MediaMetadata.Composer] as string;
        ti.conductor = md[MediaMetadata.Conductor] as string;
      }

			return ti;
		}


		/// <summary>
		/// Used internally to fill a row of a DataSet with metadata for the given media file.
		/// </summary>
		/// <param name="filename">Windows Media File (.wma file)</param></param>
		/// <param name="row">The TrackRow to be filled with data</param>
		private void RetrieveTrackRow(string filename, ref MediaData.TrackRow row)
		{
			MediaData.TrackDataTable tab = new MediaData.TrackDataTable();

			using (MetadataEditor md = new MetadataEditor(filename))
			{
        row.AlbumTitle = md[MediaMetadata.AlbumTitle] as string;
        row.AlbumArtist = md[MediaMetadata.AlbumArtist] as string;
        row.Author = md[MediaMetadata.Author] as string;

        object o = md[MediaMetadata.BitRate];
        if (o == null)
          row.SetBitRateNull();
        else
          row.BitRate = (uint)o;

        row.Composer = md[MediaMetadata.Composer] as string;
        row.Conductor = md[MediaMetadata.Conductor] as string;

        o = md[MediaMetadata.Duration];
        if (o == null)
          row.SetDurationNull();
        else
        {
          row.Duration = new TimeSpan((long)(ulong)o);
        }

        row.FileName = filename;

        o = md[MediaMetadata.FileSize];
        if (o == null)
          row.SetFileSizeNull();
        else
          row.FileSize = (ulong)o;

        row.Genre = md[MediaMetadata.Genre] as string;

        o = md[MediaMetadata.IsProtected];
        if (o == null)
          row.SetIsProtectedNull();
        else
          row.IsProtected = (bool)o;

        row.Lyrics = md[MediaMetadata.Lyrics] as string;
        row.AcoustID = md[MediaMetadata.AcoustID] as string;
        row.MBID = md[MediaMetadata.MBID] as string;
        row.Publisher = md[MediaMetadata.Publisher] as string;
        row.Text = md[MediaMetadata.Text] as string;
        row.Title = md[MediaMetadata.Title] as string;

        o = md[MediaMetadata.TrackNumber];
				if (o == null)
					row.SetTrackNumberNull();
				else
					row.TrackNumber = (uint) o;
      }
		}


		/// <summary>
		/// Retrieves media information for a single directory as a DataSet.
		/// </summary>
		/// <param name="directory">Directory to be used for data retrieval</param>
		/// <returns>MediaData object (a strongly-typed DataSet)</returns>
		public MediaData RetrieveSingleDirectoryInfo(string directory)
		{
			MediaData md = new MediaData();

			DirectoryInfo dirInfo = new DirectoryInfo(directory);
			FileInfo[] fileInfos = dirInfo.GetFiles("*.wma");
			
			for (int i=0; i < fileInfos.Length; i++)
			{
				MediaData.TrackRow row = md.Track.NewTrackRow();
				RetrieveTrackRow(dirInfo + "\\" + fileInfos[i].Name, ref row);
				md.Track.AddTrackRow(row);

				NotifyEventSubscribers(row);
			}

			return md;
		}

		/// <summary>
		/// Recursively trawls through a directory structure for media files,
		/// using them to build a DataSet of media metadata.
		/// </summary>
		/// <param name="directory">Starting point for the recursive search</param>
		/// <returns>MediaData object (a strongly-typed DataSet)</returns>
		public MediaData RetrieveRecursiveDirectoryInfo(string directory)
		{
			MediaData md = new MediaData();

			RecurseDirectories(directory, md);

			return md;
		}

		/// <summary>
		/// Internal method used to fire the TrackAdded event.
		/// </summary>
		/// <param name="row">Row that's just been added</param>
		private void NotifyEventSubscribers(MediaData.TrackRow row)
		{
			// notify any event subscribers
			if (TrackAdded != null)
			{
				TrackAdded(this, new TrackInfoEventArgs(
					(row.IsTitleNull()  ? "" : row.Title), 
					(row.IsAuthorNull() ? "" : row.Author)));
			}
		}

		/// <summary>
		/// Recursive function used by RetrieveRecursiveDirectoryInfo to drill down
		/// the directory tree.
		/// </summary>
		/// <param name="directory">Current directory level</param>
		/// <param name="md">MediaData structure in current form</param>
		private void RecurseDirectories(string directory, MediaData md)
		{
			DirectoryInfo parent = new DirectoryInfo(directory);

			DirectoryInfo[] children = parent.GetDirectories();
			foreach (DirectoryInfo folder in children)
			{
				FileInfo[] fileInfos = folder.GetFiles("*.wma");
			
				for (int i=0; i < fileInfos.Length; i++)
				{
					MediaData.TrackRow row = md.Track.NewTrackRow();
					RetrieveTrackRow(folder.FullName + "\\" + fileInfos[i].Name, ref row);
					md.Track.AddTrackRow(row);

					NotifyEventSubscribers(row);
				}

				RecurseDirectories(folder.FullName, md);
			}
		}

    //------------------------------------------------------------------------------
    // Name: ShowAttributes()
    // Desc: Displays all attributes for the specified stream.
    //------------------------------------------------------------------------------
    public NameValueCollection ShowAttributes(string pwszFileName, ushort wStreamNum)
    {
      try
      {
        NameValueCollection nvc = new NameValueCollection();

        IWMMetadataEditor mde;
        IWMHeaderInfo3 HeaderInfo3;
        ushort wAttributeCount;

        WindowsMediaWrapper.CreateEditor(out mde);

        mde.Open(pwszFileName);

        HeaderInfo3 = (IWMHeaderInfo3)mde;

        HeaderInfo3.GetAttributeCount(wStreamNum, out wAttributeCount);

        for (ushort wAttribIndex = 0; wAttribIndex < wAttributeCount; wAttribIndex++)
        {
          WMT_ATTR_DATATYPE wAttribType;
          StringBuilder pwszAttribName = null;
          byte[] pbAttribValue = null;
          ushort wAttribNameLen = 0;
          ushort wAttribValueLen = 0;

          HeaderInfo3.GetAttributeByIndex(wAttribIndex,
                                           ref wStreamNum,
                                           pwszAttribName,
                                           ref wAttribNameLen,
                                           out wAttribType,
                                           pbAttribValue,
                                           ref wAttribValueLen);

          pbAttribValue = new byte[wAttribValueLen];
          pwszAttribName = new StringBuilder(wAttribNameLen);

          HeaderInfo3.GetAttributeByIndex(wAttribIndex,
                                           ref wStreamNum,
                                           pwszAttribName,
                                           ref wAttribNameLen,
                                           out wAttribType,
                                           pbAttribValue,
                                           ref wAttribValueLen);

          string attr = AttrToStr(wAttribType, pbAttribValue, wAttribValueLen);

          nvc.Add(pwszAttribName.ToString(), attr);
        }

        return nvc;
      }
      catch
      {
        return null;
      }
    }

    //------------------------------------------------------------------------------
    // Name: ShowAttributes3()
    // Desc: Displays all attributes for the specified stream, with support
    //       for GetAttributeByIndexEx.
    //------------------------------------------------------------------------------
    public NameValueCollection ShowAttributes3(string pwszFileName, ushort wStreamNum)
    {
      try
      {
        NameValueCollection nvc = new NameValueCollection();

        IWMMetadataEditor mde;
        IWMHeaderInfo3 HeaderInfo3;
        ushort wAttributeCount = 0;

        WindowsMediaWrapper.CreateEditor(out mde);

        mde.Open(pwszFileName);

        HeaderInfo3 = (IWMHeaderInfo3)mde;

        HeaderInfo3.GetAttributeCountEx(wStreamNum, out wAttributeCount);

        for (ushort wAttribIndex = 0; wAttribIndex < wAttributeCount; wAttribIndex++)
        {
          WMT_ATTR_DATATYPE wAttribType;
          ushort wLangIndex = 0;
          StringBuilder pwszAttribName = null;
          byte[] pbAttribValue = null;
          ushort wAttribNameLen = 0;
          uint dwAttribValueLen = 0;

          HeaderInfo3.GetAttributeByIndexEx(wStreamNum,
                                             wAttribIndex,
                                             pwszAttribName,
                                             ref wAttribNameLen,
                                             out wAttribType,
                                             out wLangIndex,
                                             pbAttribValue,
                                             ref dwAttribValueLen);

          pwszAttribName = new StringBuilder(wAttribNameLen);
          pbAttribValue = new byte[dwAttribValueLen];

          HeaderInfo3.GetAttributeByIndexEx(wStreamNum,
                                             wAttribIndex,
                                             pwszAttribName,
                                             ref wAttribNameLen,
                                             out wAttribType,
                                             out wLangIndex,
                                             pbAttribValue,
                                             ref dwAttribValueLen);

          string attr = AttrToStr(wAttribType, pbAttribValue, dwAttribValueLen);

          nvc.Add(pwszAttribName.ToString(), attr);
        }
        return nvc;
      }
      catch
      {
        return null;
      }
    }

    //------------------------------------------------------------------------------
    // Name: AttrToStr()
    // Desc: Converts the attribute to a string
    //------------------------------------------------------------------------------
    private string AttrToStr(WMT_ATTR_DATATYPE AttribDataType, byte[] pbValue, uint dwValueLen)
    {
      string pwszValue = String.Empty;

      //
      // The attribute value.
      //
      switch (AttribDataType)
      {
        // String
        case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:

          if (0 == dwValueLen)
          {
            pwszValue = "null";
          }
          else
          {
            if ((0xFE == Convert.ToInt16(pbValue[0])) &&
                 (0xFF == Convert.ToInt16(pbValue[1])))
            {
              if (4 <= dwValueLen)
              {
                for (int i = 0; i < pbValue.Length - 2; i += 2)
                {
                  pwszValue += Convert.ToString(BitConverter.ToChar(pbValue, i));
                }
              }
            }
            else if ((0xFF == Convert.ToInt16(pbValue[0])) &&
                      (0xFE == Convert.ToInt16(pbValue[1])))
            {
              if (4 <= dwValueLen)
              {
                for (int i = 0; i < pbValue.Length - 2; i += 2)
                {
                  pwszValue += Convert.ToString(BitConverter.ToChar(pbValue, i));
                }
              }
            }
            else
            {
              if (2 <= dwValueLen)
              {
                for (int i = 0; i < pbValue.Length - 2; i += 2)
                {
                  pwszValue += Convert.ToString(BitConverter.ToChar(pbValue, i));
                }
              }
            }
          }
          break;

        // Binary
        case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:

          pwszValue = dwValueLen.ToString();
          break;

        // Boolean
        case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:

          if (BitConverter.ToBoolean(pbValue, 0))
          {
            pwszValue = "true";
          }
          else
          {
            pwszValue = "false";
          }
          break;

        // DWORD
        case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:

          uint dwValue = BitConverter.ToUInt32(pbValue, 0);
          pwszValue = dwValue.ToString();
          break;

        // QWORD
        case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:

          ulong qwValue = BitConverter.ToUInt64(pbValue, 0);
          pwszValue = qwValue.ToString();
          break;

        // WORD
        case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:

          uint wValue = BitConverter.ToUInt16(pbValue, 0);
          pwszValue = wValue.ToString();
          break;

        // GUID
        case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:

          pwszValue = BitConverter.ToString(pbValue, 0, pbValue.Length);
          break;

        default:

          break;
      }

      return pwszValue;
    }
    //------------------------------------------------------------------------------
    // Name: DeleteAttrib()
    // Desc: Delete the attribute at the specified index.
    //------------------------------------------------------------------------------
    public bool DeleteAttrib(string pwszFileName, ushort wStreamNum, ushort wAttribIndex)
    {
      try
      {
        IWMMetadataEditor mde;

        WindowsMediaWrapper.CreateEditor(out mde);

        mde.Open(pwszFileName);

        IWMHeaderInfo3 hi3 = (IWMHeaderInfo3)mde;

        hi3.DeleteAttribute(wStreamNum, wAttribIndex);

        mde.Flush();

        mde.Close();
      }
      catch
      {
        return false;
      }

      return true;
    }
    //------------------------------------------------------------------------------
    // Name: DeleteAllAttrib()
    // Desc: Delete all attributes.
    //------------------------------------------------------------------------------
    // You can use 0xFFFF for the stream number to specify an attribute using its
    // global index. Global index values range from 0 to one less than the count of
    // attributes received from a call to IWMHeaderInfo3::GetAttributeCountEx where
    // the stream number was set to 0xFFFF.
    //
    // When deleting multiple attributes, you should do so in descending order by
    // index value. For convenience, this is the order in which index values are
    // retrieved by IWMHeaderInfo3::GetAttributeIndices.
    public int DeleteAllAttrib(string pwszFileName)
    {
      try
      {
        IWMMetadataEditor mde;

        WindowsMediaWrapper.CreateEditor(out mde);

        if (mde == null) return -1;

        mde.Open(pwszFileName);

        try
        {
          IWMHeaderInfo3 hi3 = (IWMHeaderInfo3)mde;

          // wStreamNum: Pass zero to retrieve the count of attributes that apply to the file rather than a
          // specific stream. Pass 0xFFFF to retrieve the total count of all attributes in the file, both
          // stream-specific and file-level.
          ushort attrCount;
          hi3.GetAttributeCountEx(0xffff, out attrCount); // Use 0xffff to get global count

          // Go in decending order
          // Returns in HRESULT = S_OK if success
          if (attrCount > 0)
          {
            for (int ii = attrCount - 1; ii >= 0; ii--)
            {
              try
              {
                // use 0xffff to use 0-based global index
                uint r = hi3.DeleteAttribute(0xffff, (ushort)ii);
                if (r != 0) return -2;
              }
              catch
              {
                //return -3;
              }
            }
          }

          return attrCount;
        }
        finally
        {
          mde.Flush();
          mde.Close();
        }
      }
      catch
      {
        return -4;
      }
    }
    //------------------------------------------------------------------------------
    // Name: AddAttrib()
    // Desc: Add an attribute with the specifed language index.
    //------------------------------------------------------------------------------
    public bool AddAttrib(string pwszFileName, ushort wStreamNum, string pwszAttribName,
                    ushort wAttribType, string pwszAttribValue, ushort wLangIndex)
    {
      try
      {
        IWMMetadataEditor mde;
        IWMHeaderInfo3 HeaderInfo3;
        byte[] pbAttribValue;
        int nAttribValueLen;
        WMT_ATTR_DATATYPE AttribDataType = (WMT_ATTR_DATATYPE)wAttribType;
        ushort wAttribIndex = 0;

        if (!TranslateAttrib(AttribDataType, pwszAttribValue, out pbAttribValue, out nAttribValueLen))
        {
          return false;
        }

        WindowsMediaWrapper.CreateEditor(out mde);

        mde.Open(pwszFileName);

        HeaderInfo3 = (IWMHeaderInfo3)mde;

        HeaderInfo3.AddAttribute(wStreamNum,
                                  pwszAttribName,
                                  out wAttribIndex,
                                  AttribDataType,
                                  wLangIndex,
                                  pbAttribValue,
                                  (uint)nAttribValueLen);

        mde.Flush();

        mde.Close();
      }
      catch
      {
        return (false);
      }

      return (true);
    }

    //------------------------------------------------------------------------------
    // Name: ModifyAttrib()
    // Desc: Modifies the value of the specified attribute.
    //------------------------------------------------------------------------------
    public bool ModifyAttrib(string pwszFileName, ushort wStreamNum, ushort wAttribIndex,
                       ushort wAttribType, string pwszAttribValue, ushort wLangIndex)
    {
      try
      {
        IWMMetadataEditor mde;
        IWMHeaderInfo3 HeaderInfo3;
        byte[] pbAttribValue;
        int nAttribValueLen;
        WMT_ATTR_DATATYPE AttribDataType = (WMT_ATTR_DATATYPE)wAttribType;

        if (!TranslateAttrib(AttribDataType, pwszAttribValue, out pbAttribValue, out nAttribValueLen))
        {
          return false;
        }

        WindowsMediaWrapper.CreateEditor(out mde);

        mde.Open(pwszFileName);

        HeaderInfo3 = (IWMHeaderInfo3)mde;

        HeaderInfo3.ModifyAttribute(wStreamNum,
                                     wAttribIndex,
                                     AttribDataType,
                                     wLangIndex,
                                     pbAttribValue,
                                     (uint)nAttribValueLen);

        mde.Flush();

        mde.Close();
      }
      catch
      {
        return (false);
      }

      return (true);
    }

    //------------------------------------------------------------------------------
    // Name: SetAttrib()
    // Desc: Set the specified attribute.
    //------------------------------------------------------------------------------
    public bool SetAttrib(string pwszFileName, ushort wStreamNum, string pwszAttribName,
                    ushort wAttribType, string pwszAttribValue)
    {
      try
      {
        IWMMetadataEditor mde;
        IWMHeaderInfo3 HeaderInfo3;
        byte[] pbAttribValue;
        int nAttribValueLen;
        WMT_ATTR_DATATYPE AttribDataType = (WMT_ATTR_DATATYPE)wAttribType;

        if (!TranslateAttrib(AttribDataType, pwszAttribValue, out pbAttribValue, out nAttribValueLen))
        {
          return false;
        }

        WindowsMediaWrapper.CreateEditor(out mde);

        mde.Open(pwszFileName);

        HeaderInfo3 = (IWMHeaderInfo3)mde;

        HeaderInfo3.SetAttribute(wStreamNum,
                                  pwszAttribName,
                                  AttribDataType,
                                  pbAttribValue,
                                  (ushort)nAttribValueLen);

        mde.Flush();

        mde.Close();
      }
      catch
      {
        return (false);
      }

      return (true);
    }

    //------------------------------------------------------------------------------
    // Name: TranslateAttrib()
    // Desc: Converts attributes to byte arrays.
    //------------------------------------------------------------------------------
    private bool TranslateAttrib(WMT_ATTR_DATATYPE AttribDataType,
                            string pwszValue, out byte[] pbValue, out int nValueLength)
    {
      switch (AttribDataType)
      {
        case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:

          nValueLength = 4;
          uint[] pdwAttribValue = new uint[1] { Convert.ToUInt32(pwszValue) };

          pbValue = new Byte[nValueLength];
          Buffer.BlockCopy(pdwAttribValue, 0, pbValue, 0, nValueLength);

          return (true);

        case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:

          nValueLength = 2;
          ushort[] pwAttribValue = new ushort[1] { Convert.ToUInt16(pwszValue) };

          pbValue = new Byte[nValueLength];
          Buffer.BlockCopy(pwAttribValue, 0, pbValue, 0, nValueLength);

          return (true);

        case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:

          nValueLength = 8;
          ulong[] pqwAttribValue = new ulong[1] { Convert.ToUInt64(pwszValue) };

          pbValue = new Byte[nValueLength];
          Buffer.BlockCopy(pqwAttribValue, 0, pbValue, 0, nValueLength);

          return (true);

        case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:

          nValueLength = (ushort)((pwszValue.Length + 1) * 2);
          pbValue = new Byte[nValueLength];

          Buffer.BlockCopy(pwszValue.ToCharArray(), 0, pbValue, 0, pwszValue.Length * 2);
          pbValue[nValueLength - 2] = 0;
          pbValue[nValueLength - 1] = 0;

          return (true);

        case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:

          nValueLength = 4;
          pdwAttribValue = new uint[1] { Convert.ToUInt32(pwszValue) };
          if (pdwAttribValue[0] != 0)
          {
            pdwAttribValue[0] = 1;
          }

          pbValue = new Byte[nValueLength];
          Buffer.BlockCopy(pdwAttribValue, 0, pbValue, 0, nValueLength);

          return (true);

        default:

          pbValue = null;
          nValueLength = 0;

          return (false);
      }
    }
  }
}
