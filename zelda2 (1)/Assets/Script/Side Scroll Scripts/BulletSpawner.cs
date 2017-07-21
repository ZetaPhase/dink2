using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
	[SerializeField]
	private GameObject _projectilePrefab;

	private List<GameObject> _projectiles = new List<GameObject> ();

	[SerializeField]
	private bool _facingRight;

	[SerializeField]
	private Vector3 offset;
	void Update()
	{
		handleBullet ();
	}

	void handleBullet()
	{
		if (_projectiles.Count <= 5 /*&& (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") ||_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) */) {

			GameObject bullet = (GameObject)Instantiate (_projectilePrefab, transform.position + offset, Quaternion.identity);

			_projectiles.Add (bullet);
			AudioSource audio = GetComponent<AudioSource> ();

			bullet.GetComponent<BulletForce> ().setOrientation (_facingRight);

			audio.Play ();
		}
		for (int i = 0; i < _projectiles.Count; i++) {
			GameObject currentBullet = _projectiles [i];

			if (currentBullet == null)
				_projectiles.RemoveAt(i);

		}
	}
}
