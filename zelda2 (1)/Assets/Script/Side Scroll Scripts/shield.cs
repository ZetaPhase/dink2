using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
	private HashSet<GameObject> collisions = new HashSet<GameObject> ();

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "alien_weapon" || other.tag == "PlayerAttack") {
			collisions.Add (other.gameObject);
		}
		if (other.tag == "projectile") {
			Destroy (other.gameObject);
		}
	}

	public bool blocked (GameObject other)
	{
		if (collisions.Contains (other)) {
			//if (this.gameObject.name == "Dink") {
				Debug.Log ("blocked");
				this.GetComponent<AudioSource> ().Play ();
			//}
			return true;
		}
		return false;
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (collisions.Contains(other.gameObject)){
			if (this.gameObject.activeInHierarchy) {
				StartCoroutine (rem (other.gameObject));
			}
		}
	}

	void OnDissable ()
	{
		collisions = new HashSet<GameObject> ();
	}

	IEnumerator rem (GameObject other)
	{
		yield return new WaitForFixedUpdate ();
		yield return new WaitForFixedUpdate ();
		collisions.Remove (other);
		yield return null;
	}
}
