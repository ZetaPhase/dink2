using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	// Use this for initialization
	void Update () {
		this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 3, 0);
	}
}
