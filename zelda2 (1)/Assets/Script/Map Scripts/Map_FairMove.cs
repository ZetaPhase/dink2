using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_FairMove : MonoBehaviour {
	[SerializeField]
	private float _moveSpeed;

	private float _delayBetweenPushes = 1F;
	[SerializeField]
	private Rigidbody2D _rb;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine (move ());
		_rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator move()
	{
		Vector2 moveDir = new Vector2 (Random.Range (-1F, 1F), Random.Range (-1F, 1F));
		_rb.AddForce (moveDir * _moveSpeed);
		yield return new WaitForSeconds (_delayBetweenPushes);
		StartCoroutine (move ());
		yield return null;
	}
}
