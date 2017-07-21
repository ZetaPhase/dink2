using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_EnemyController : MonoBehaviour {
	//TODO: Enemies are able to spawn inside of the wall tiles currently

	[SerializeField]
	private int _numberAtATime;

	private int _curEnemy;

	[SerializeField]
	[Tooltip("All the prefabs for the overworld enemies")]
	private GameObject[] _enemys;

	[SerializeField]
	[Tooltip("How far away from the player enemies will be spawned")]
	private float _range = 1F;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		spawnEnemies();
	}

	void spawnEnemies()
	{
		for (int i = 0; i < (_numberAtATime - _curEnemy); i++) 
		{
			bool isOk = false;
			Vector3 tempPos = new Vector3 ();
			while (!isOk) {
				tempPos = new Vector3 (Random.Range (-1, 1), Random.Range (-1, 1), 0F);
				if (tempPos != Vector3.zero) {
					isOk = true;
				}
			}
			Instantiate (_enemys [Random.Range (0, _enemys.Length)], tempPos * _range, Quaternion.identity);
			_curEnemy++;
		}
	}
	public void removeEnemy()
	{
		_curEnemy--;
	}
}
