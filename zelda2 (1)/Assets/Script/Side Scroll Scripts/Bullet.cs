using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {	
	[SerializeField]
	private float damage;
	void OnTriggerEnter2D(Collider2D other) {
		//if(other.gameObject.name == "False Dink")
		//{
		//	other.GetComponent<BossBehavior> ().takeDamage (damage);
		if (!(other.gameObject.name == "False Dink") && other.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			other.GetComponent<Enemy> ().takeDamage (damage);
		}
	}
}
