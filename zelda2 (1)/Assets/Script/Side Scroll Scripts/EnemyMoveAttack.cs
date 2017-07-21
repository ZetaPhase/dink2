using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAttack : MonoBehaviour {

	public enum movementType{Purse,Wander}
	[SerializeField]
	private movementType moveType;
	[SerializeField]
	private bool isMovingLeft;

	[SerializeField]
	private float _damage;

	[SerializeField]
	private float _moveSpeed;
	private Rigidbody2D _rb;

	[SerializeField]
	private float _timeBetweenSwitch;

//	private GameObject _player;
//
//	[SerializeField]
//	private float _attackRange = 2F;
//
//	[SerializeField]
//	private float _timeBetweenAttacks;
	// Use this for initialization
	void Start () 
	{
		_rb = GetComponent<Rigidbody2D> ();
		//StartCoroutine (attack ());
		StartCoroutine (switchDirection ());
//		_player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		if (moveType == movementType.Wander) 
		{
			Wander ();
		}else if(moveType == movementType.Purse)
		{
			Purse ();
		}
	}

	void Purse()
	{

	}

	void Wander()
	{
		if(isMovingLeft)
		{
			_rb.velocity = new Vector2 (-1 * _moveSpeed, _rb.velocity.y);
		}else{
			_rb.velocity = new Vector2 (1 * _moveSpeed, _rb.velocity.y);	
		}
	}
	IEnumerator switchDirection()
	{
		yield return new WaitForSeconds (_timeBetweenSwitch);
		isMovingLeft = !isMovingLeft;
		StartCoroutine(switchDirection());
		yield return null;
	}
	/*
	IEnumerator attack()
	{
		yield return new WaitForSeconds (_timeBetweenAttacks);

		if(Vector3.Distance(transform.position,_player.transform.position) < _attackRange)
		{
			_player.GetComponent<PlayerHealthHandler> ().takeDamage (_damage);
		}
		StartCoroutine (attack ());
		yield return null;
	}
	*/
}