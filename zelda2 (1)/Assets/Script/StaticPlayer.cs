using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPlayer : object {
	//Auto level load
	public static Vector3 lastPos;
	public static Vector3 TelePos;
	public static string Last;

	public static Vector3 spawnPosition = Vector3.forward;
	public static float health = Config.MAX_HEALTH;
	public static float musicTime = 0;

	public static float lastKnownHealth;
	//used for music time
	public static float worldTime = 0;
	//0 health //1 strength //2 magic
	//public static float[] levels;
	public static bool[] dungeons = new bool[2];


	public static int maxLives = 5;
	public static int lives = maxLives;
	public static bool AllDung()
	{
		
		for (int i = 0; i < dungeons.Length; i++) {
			Debug.Log ("Hi");
			if (!dungeons [i]) {
				Debug.Log ("false");
				return false;
			}
		}
		Debug.Log ("true");
		return true;
	}
	public static void setHealth(float h){
		health = h;
	}

	public static float getHealth(){
		return health;
	}

	public static void resetLiveCounter()
	{
		lives = maxLives;
	}
}
