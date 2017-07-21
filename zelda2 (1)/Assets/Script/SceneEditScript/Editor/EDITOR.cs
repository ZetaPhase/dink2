using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//[CanEditMultipleObjects]
[CustomEditor (typeof (ASTEROIDBUILDER))]
class EDITOR : Editor {
	GameObject[] asteroids = new GameObject[5];
	int place;
	float r;
	float ir;
	// Update is called once per frame
	public override void OnInspectorGUI()
	{
		//ScriptableObject obj = ScriptableObject.CreateInstance<ScriptableObject> (); 
		asteroids[0] = ((GameObject) EditorGUILayout.ObjectField(asteroids[0], typeof(GameObject), false, new GUILayoutOption[0]));
		asteroids[1] = ((GameObject) EditorGUILayout.ObjectField(asteroids[1], typeof(GameObject), false, new GUILayoutOption[0]));
		asteroids[2] = ((GameObject) EditorGUILayout.ObjectField(asteroids[2], typeof(GameObject), false, new GUILayoutOption[0]));
		asteroids[3] = ((GameObject) EditorGUILayout.ObjectField(asteroids[3], typeof(GameObject), false, new GUILayoutOption[0]));
		asteroids[4] = ((GameObject) EditorGUILayout.ObjectField(asteroids[4], typeof(GameObject), false, new GUILayoutOption[0]));
		//asteroids[5] = ((GameObject) EditorGUILayout.ObjectField(asteroids[5], typeof(GameObject), false, new GUILayoutOption[0]));
		//asteroids[6] = ((GameObject) EditorGUILayout.ObjectField(asteroids[6], typeof(GameObject), false, new GUILayoutOption[0]));
		//asteroids[0] = ((GameObject) EditorGUILayout.ObjectField (asteroids[0], GameObject, false, new GUILayoutOption[0]));
		r = EditorGUILayout.FloatField(r, new GUILayoutOption[0]);
		ir = EditorGUILayout.FloatField(ir, new GUILayoutOption[0]);
		place = EditorGUILayout.IntField (place, new GUILayoutOption[0]);
		if (place == 42) {
			Instantiate (asteroids [Random.Range (0, asteroids.Length)], Vector3.Scale (new Vector3 (1, 1, 0), Random.onUnitSphere).normalized * Random.Range(ir, r), Quaternion.Euler (0, 0, Random.Range (0, 360)));
		}
	}
}
