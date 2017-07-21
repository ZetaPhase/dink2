using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ASTEROIDBUILDER : MonoBehaviour {

	void OnDrawGizmos () {
		if (Input.GetKey(KeyCode.Space))
		{
			Debug.Log ("space");
		}
	}
}
