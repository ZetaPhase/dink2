using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

	public static string HORIZONTAL_INPUT = "Horizontal";
	public static string VERTICAL_INPUT = "Vertical";
	public static float COLLISON_CHECK_DISTANCE = 1F;

	public static float MAX_HEALTH = 5F;

	public static Vector2 ConvertV3ToV2(Vector3 v3)
	{
		return new Vector2(v3.x,v3.y);
	}
	public static Vector3 ConvertV2ToV3(Vector2 v2)
	{
		return new Vector3 (v2.x, v2.y, 0F);
	}
	public static Vector3 ConvertV2ToV3(Vector2 v2,float depth)
	{
		return new Vector3 (v2.x, v2.y, depth);
	}

}
