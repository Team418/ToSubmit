using UnityEngine;
using System.IO;
using System;

public class TurretChars : MonoBehaviour 
{
	public static float[] Damage;
	public static float[] SPS;//Shoots per second
	public static bool DCReady = false;
    public static int N;
    string path;

    public GameObject[] turretPrefabs = new GameObject[N+1];

	void InitStats()
	{
        StreamReader sr = File.OpenText(path);

		N = Convert.ToInt32 (sr.ReadLine());
		Damage = new float[N + 1];
		SPS = new float[N + 1];

		for (int i = 1; i <= N; i++) 
		{
			string[] s = sr.ReadLine ().Split (' ');
			Damage [i] = Convert.ToSingle(s [0]);
			SPS [i] = Convert.ToSingle(s [1]);
		}
		sr.Close();
	}

	public static int NumberOfType(string type)//See codificator
	{
		switch (type) 
		{
		case "Wall": return 1;
		case "Tesla Tree": return 2;
		case "Railgun":	return 3;
		case "Plasma_Tower": return 4;
		case "Mortar": return 5;
		case "Generator": return 6;
		case "Freeze_Tower": return 7;
		case "Flame_Tower": return 8;
		case "Buffer": return 9;
		case "Anti-Air": return 10;
		case "Skeleton_Tower": return 11;
		case "Main_Tower": return 12;
		default: return -1;
		}
	}

	void Start()
	{
        path = Application.dataPath + "/UnitsChars/Turret.chars";
        InitStats ();
		DCReady = true;
	}
}
