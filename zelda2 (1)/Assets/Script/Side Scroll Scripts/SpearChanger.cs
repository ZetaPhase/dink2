using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearChanger : MonoBehaviour {

	[SerializeField]
	private GameObject[] locations;

	[SerializeField]
	private GameObject spear;

	private BoxCollider2D _bC2;
	// Use this for initialization
	void Start () {
		_bC2 = spear.GetComponent<BoxCollider2D> ();
		StartCoroutine (waitAndDeploySpear ());
	}
	
	IEnumerator waitAndDeploySpear()
	{
		yield return new WaitForSeconds (1F);
		int rando = Random.Range (0, 3);
		spear.transform.position = locations [rando].transform.position;
		_bC2.enabled = true;
		yield return new WaitForSeconds (.4F);
		spear.transform.position = locations [rando].transform.position + new Vector3 (-1F, 0F, 0F);
		yield return new WaitForSeconds (.4F);
		_bC2.enabled = false;
		spear.transform.position = locations [rando].transform.position;
		StartCoroutine(waitAndDeploySpear ());
	}
}
