using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_EnemyMove : MonoBehaviour {

	[SerializeField]
	private float _moveSpeed = 5F;

	private GameObject _player;

	private float _stopRange = 1F;

	private float _distanceToPlayer;

	[SerializeField]
	private float _maxDistance = 10F;

	[SerializeField]
	private float _timeInMap;

	private Map_EnemyController _enemyControler;

	// Use this for initialization
	void Awake ()
	{
		_player = GameObject.Find ("Player");
		_enemyControler = _player.GetComponent<Map_EnemyController> ();
		StartCoroutine (waitAndDestroy ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		// TODO: Make enemy avoid objects

		_distanceToPlayer = Vector3.Distance (transform.position, _player.transform.position);
		if (_distanceToPlayer > _maxDistance) 
		{

		}else if(_distanceToPlayer > _stopRange)
		{
			transform.position = Vector3.MoveTowards (transform.position, _player.transform.position, _moveSpeed * Time.deltaTime);
		}
	}
	IEnumerator waitAndDestroy()
	{
		yield return new WaitForSecondsRealtime (_timeInMap);
		_enemyControler.removeEnemy ();
		Destroy (this.gameObject);
	}
}
