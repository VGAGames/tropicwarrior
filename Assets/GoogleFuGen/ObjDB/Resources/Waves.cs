//----------------------------------------------
//    GoogleFu: Google Doc Unity integration
//         Copyright © 2013 Litteratus
//
//        This file has been auto-generated
//              Do not manually edit
//----------------------------------------------

using UnityEngine;

namespace GoogleFu
{
	[System.Serializable]
	public class WavesRow 
	{
		public string _NAMEENEMY0;
		public string _NAMEENEMY1;
		public string _NAMEENEMY2;
		public string _NAMEENEMY3;
		public string _NAMEENEMY4;
		public string _NAMEENEMY5;
		public string _NAMEENEMY6;
		public string _NAMEENEMY7;
		public string _NAMEENEMY8;
		public string _NAMEENEMY9;
		public WavesRow(string __NAMEENEMY0, string __NAMEENEMY1, string __NAMEENEMY2, string __NAMEENEMY3, string __NAMEENEMY4, string __NAMEENEMY5, string __NAMEENEMY6, string __NAMEENEMY7, string __NAMEENEMY8, string __NAMEENEMY9) 
		{
			_NAMEENEMY0 = __NAMEENEMY0;
			_NAMEENEMY1 = __NAMEENEMY1;
			_NAMEENEMY2 = __NAMEENEMY2;
			_NAMEENEMY3 = __NAMEENEMY3;
			_NAMEENEMY4 = __NAMEENEMY4;
			_NAMEENEMY5 = __NAMEENEMY5;
			_NAMEENEMY6 = __NAMEENEMY6;
			_NAMEENEMY7 = __NAMEENEMY7;
			_NAMEENEMY8 = __NAMEENEMY8;
			_NAMEENEMY9 = __NAMEENEMY9;
		}

		public int Length { get { return 10; } }

		public string this[int i]
		{
		    get
		    {
		        return GetStringDataByIndex(i);
		    }
		}

		public string GetStringDataByIndex( int index )
		{
			string ret = System.String.Empty;
			switch( index )
			{
				case 0:
					ret = _NAMEENEMY0.ToString();
					break;
				case 1:
					ret = _NAMEENEMY1.ToString();
					break;
				case 2:
					ret = _NAMEENEMY2.ToString();
					break;
				case 3:
					ret = _NAMEENEMY3.ToString();
					break;
				case 4:
					ret = _NAMEENEMY4.ToString();
					break;
				case 5:
					ret = _NAMEENEMY5.ToString();
					break;
				case 6:
					ret = _NAMEENEMY6.ToString();
					break;
				case 7:
					ret = _NAMEENEMY7.ToString();
					break;
				case 8:
					ret = _NAMEENEMY8.ToString();
					break;
				case 9:
					ret = _NAMEENEMY9.ToString();
					break;
			}

			return ret;
		}

		public string GetStringData( string colID )
		{
			string ret = System.String.Empty;
			switch( colID.ToUpper() )
			{
				case "NAMEENEMY0":
					ret = _NAMEENEMY0.ToString();
					break;
				case "NAMEENEMY1":
					ret = _NAMEENEMY1.ToString();
					break;
				case "NAMEENEMY2":
					ret = _NAMEENEMY2.ToString();
					break;
				case "NAMEENEMY3":
					ret = _NAMEENEMY3.ToString();
					break;
				case "NAMEENEMY4":
					ret = _NAMEENEMY4.ToString();
					break;
				case "NAMEENEMY5":
					ret = _NAMEENEMY5.ToString();
					break;
				case "NAMEENEMY6":
					ret = _NAMEENEMY6.ToString();
					break;
				case "NAMEENEMY7":
					ret = _NAMEENEMY7.ToString();
					break;
				case "NAMEENEMY8":
					ret = _NAMEENEMY8.ToString();
					break;
				case "NAMEENEMY9":
					ret = _NAMEENEMY9.ToString();
					break;
			}

			return ret;
		}
		public override string ToString()
		{
			string ret = System.String.Empty;
			ret += "{" + "NAMEENEMY0" + " : " + _NAMEENEMY0.ToString() + "} ";
			ret += "{" + "NAMEENEMY1" + " : " + _NAMEENEMY1.ToString() + "} ";
			ret += "{" + "NAMEENEMY2" + " : " + _NAMEENEMY2.ToString() + "} ";
			ret += "{" + "NAMEENEMY3" + " : " + _NAMEENEMY3.ToString() + "} ";
			ret += "{" + "NAMEENEMY4" + " : " + _NAMEENEMY4.ToString() + "} ";
			ret += "{" + "NAMEENEMY5" + " : " + _NAMEENEMY5.ToString() + "} ";
			ret += "{" + "NAMEENEMY6" + " : " + _NAMEENEMY6.ToString() + "} ";
			ret += "{" + "NAMEENEMY7" + " : " + _NAMEENEMY7.ToString() + "} ";
			ret += "{" + "NAMEENEMY8" + " : " + _NAMEENEMY8.ToString() + "} ";
			ret += "{" + "NAMEENEMY9" + " : " + _NAMEENEMY9.ToString() + "} ";
			return ret;
		}
	}
	public class Waves :  GoogleFuComponentBase
	{
		public enum rowIds {
			WAVE_1, WAVE_2, WAVE_3, WAVE_4, WAVE_5, WAVE_6
		};
		public string [] rowNames = {
			"WAVE_1", "WAVE_2", "WAVE_3", "WAVE_4", "WAVE_5", "WAVE_6"
		};
		public System.Collections.Generic.List<WavesRow> Rows = new System.Collections.Generic.List<WavesRow>();
		public override void AddRowGeneric (System.Collections.Generic.List<string> input)
		{
			Rows.Add(new WavesRow(input[0],input[1],input[2],input[3],input[4],input[5],input[6],input[7],input[8],input[9]));
		}
		public override void Clear ()
		{
			Rows.Clear();
		}
		public WavesRow GetRow(rowIds rowID)
		{
			WavesRow ret = null;
			try
			{
				ret = Rows[(int)rowID];
			}
			catch( System.Collections.Generic.KeyNotFoundException ex )
			{
				Debug.LogError( rowID + " not found: " + ex.Message );
			}
			return ret;
		}
		public WavesRow GetRow(string rowString)
		{
			WavesRow ret = null;
			try
			{
				ret = Rows[(int)System.Enum.Parse(typeof(rowIds), rowString)];
			}
			catch(System.ArgumentException) {
				Debug.LogError( rowString + " is not a member of the rowIds enumeration.");
			}
			return ret;
		}

	}

}
