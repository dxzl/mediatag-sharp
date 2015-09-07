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
	public class Attribute
	{
		private readonly string name;
		private readonly object val;

		public Attribute(string attributeName, object attributeValue)
		{
			name = attributeName;
			val = attributeValue;
		}

		public string Name
		{
			get { return name; }
		}

		public object Value
		{
			get { return val; }
		}
	}
}
