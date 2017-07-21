using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {
	[SerializeField]
	private shield _shield;
	[SerializeField]
	private PlayerHealthHandler health;
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "alien_weapon") {
			StartCoroutine(check(other.gameObject));
			return;
		}
		if (other.tag == "projectile") {
			Destroy (other.gameObject);
			health.takeDamage ();
		}
		if (other.tag == "alien") {
			health.takeDamage();
		}
	}
	IEnumerator check (GameObject other)
	{
		yield return new WaitForFixedUpdate ();
		if (!_shield.blocked (other)) {
			health.takeDamage();
		}
		yield return null;
	}
}
