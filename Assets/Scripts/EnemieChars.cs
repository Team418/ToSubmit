using UnityEngine;
using System.IO;
using System;

public class EnemieChars : MonoBehaviour 
{
	public static float[] Speed;
	public static float[] HP;
	public static bool DCReady = false;
    string path;

	void InitStats()
	{
		StreamReader sr = File.OpenText (path);

		int N = Convert.ToInt32(sr.ReadLine());
		Speed = new float[N+1];
		HP = new float[N+1];
        
		for (int i = 1; i <= N; i++) 
		{
			string[] s = sr.ReadLine ().Split(' ');
			HP [i] = Convert.ToSingle(s [0]);
			Speed [i] = Convert.ToSingle(s [1]);
		}
		sr.Close ();
	}

	public static int NumberOfType(string type)//See codificator
	{
		switch (type) 
		{
		case "Standart_Unit_No_1": return 1;
		case "Standart_Unit_No_2": return 2;
		case "Standart_Unit_No_3": return 3;
		case "Sprinter": return 4;
		case "Flying_unit": return 5;
		case "Tank_No_1": return 6;
		case "Tank_No_2": return 7;
		case "Healer": return 8;
		case "Protector": return 9;
		case "Booster": return 10;
		case "The Boss": return 11;
		default: return 0;
		}
	}

	void Start()
	{
        path = Application.dataPath + "/UnitsChars/Enemy.chars";
        InitStats();

		DCReady = true;
	}

}
