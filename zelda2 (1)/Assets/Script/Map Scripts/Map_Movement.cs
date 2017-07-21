using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Movement : MonoBehaviour {
	/*
	enum moveDir{Left,Right,Up,Down,
				UpRight,UpLeft,DownRight,DownLeft,
				None}

	private moveDir _moveDirection;
*/
	private string _inputAxisH = Config.HORIZONTAL_INPUT;
	private string _inputAxisV = Config.VERTICAL_INPUT;

	[SerializeField]
	[Tooltip("How fast the player moves around")]
	private float _movementSpeed = 5F;

	private Rigidbody2D _rb;

	// Use this for initialization
	void Start () 
	{
		if (StaticPlayer.spawnPosition.z == 0) {
			this.transform.position = StaticPlayer.spawnPosition;
		}
		//_moveDirection = moveDir.None;	
		_rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Get Input
		float inputH = Input.GetAxis (_inputAxisH);
		float inputV = Input.GetAxis (_inputAxisV);
		//Get move direction from all that
		//_moveDirection = getMoveDirFromInput (inputH, inputV);

		_rb.velocity = new Vector2(inputH,inputV).normalized * _movementSpeed;
		float zTheta = Mathf.Atan2 (_rb.velocity.y, _rb.velocity.x);
		this.transform.rotation = Quaternion.Lerp(Quaternion.Euler (0, 0, 180 / Mathf.PI * (zTheta - Mathf.PI/2)), this.transform.rotation, 0.5f);
	}
	/*
	moveDir getMoveDirFromInput(float inputH, float inputV)
	{
		// Return a move Direction based on what the inputs are
		if(inputH > 0F)
		{
			if(inputV > 0F)
			{
				return moveDir.UpRight;
			}else if (inputV < 0F)
			{
				return moveDir.DownLeft;
			}else
			{
				return moveDir.Right;
			}
		}else if(inputH < 0F)
		{
			if(inputV > 0F)
			{
				return moveDir.UpLeft;
			}else if (inputV < 0F)
			{
				return moveDir.DownLeft;
			}else
			{
				return moveDir.Left;
			}
		}else
		{
			if(inputV > 0F)
			{
				return moveDir.Up;
			}else if (inputV < 0F)
			{
				return moveDir.Down;
			}else
			{
				return moveDir.None;
			}
		}
	}*/
}
