using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseHitBox : MonoBehaviour {
	[SerializeField]
	private falseshield _shield;
	[SerializeField]
	private BossBehavior health;
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "PlayerAttack" || other.tag == "PlayerProj") {
			StartCoroutine(check(other.gameObject));
			return;
		}
	}
	IEnumerator check (GameObject other)
	{
		yield return new WaitForFixedUpdate ();
		if (!_shield.blocked (other)) {
			health.takeDamage (1);
		} else {
			Debug.Log ("blocked");
		}
		yield return null;
	}
}
