using UnityEngine;
using UnityEditor;

namespace GoogleFu
{
	[CustomEditor(typeof(Waves))]
	public class WavesEditor : Editor
	{
		public int Index = 0;
		public override void OnInspectorGUI ()
		{
			Waves s = target as Waves;
			WavesRow r = s.Rows[ Index ];

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button("<<") )
			{
				Index = 0;
			}
			if ( GUILayout.Button("<") )
			{
				Index -= 1;
				if ( Index < 0 )
					Index = s.Rows.Count - 1;
			}
			if ( GUILayout.Button(">") )
			{
				Index += 1;
				if ( Index >= s.Rows.Count )
					Index = 0;
			}
			if ( GUILayout.Button(">>") )
			{
				Index = s.Rows.Count - 1;
			}

			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "ID", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.LabelField( s.rowNames[ Index ] );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY0", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY0 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY1", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY1 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY2", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY2 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY3", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY3 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY4", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY4 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY5", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY5 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY6", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY6 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY7", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY7 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY8", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY8 );
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "NAMEENEMY9", GUILayout.Width( 150.0f ) );
			{
				EditorGUILayout.TextField( r._NAMEENEMY9 );
			}
			EditorGUILayout.EndHorizontal();

		}
	}
}
