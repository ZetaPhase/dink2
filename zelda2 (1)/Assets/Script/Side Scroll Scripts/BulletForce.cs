using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForce : MonoBehaviour {

	private Rigidbody2D _rb;

	[SerializeField]
	private Vector2 velocity;

	[SerializeField]
	private float _timeAlive;

	private bool isFacingRight;
	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D> ();
		DestroyObject (this.gameObject, _timeAlive);
	}
	
	// Update is called once per frame
	void Update () 
	{
		_rb.velocity = velocity;
	}

	public void setOrientation(bool dir)
	{
		//true = right,false = left
		if(dir == false)
		{
			velocity = new Vector2 (-velocity.x, velocity.y);
			this.transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
}
