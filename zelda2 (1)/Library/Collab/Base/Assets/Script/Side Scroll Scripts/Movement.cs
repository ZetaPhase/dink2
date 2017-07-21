using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour {
	private Vector3 shieldPos;

	private Rigidbody2D _mRigidBody;
	[SerializeField]
	private GameObject shield;
	[SerializeField]
	private float _moveSpeed;

	private bool _facingRight = true;

	private int _playerJumpPower = 400;

	[SerializeField]
	private GameObject _projectilePrefab;

	private List<GameObject> _projectiles = new List<GameObject> ();
	private List<int> _bulletCounter = new List<int> ();

	private float projectileVelocity = 10F;
	private bool bulletMovingRight = true;

	private Animator _animator;

	private PlayerHealthHandler healthHandler;

	//[SerializeField]
	//private GameObject camera;

	public bool isEnabled = true;

	private float _kickZoneDeactDelay = .1F;

	private GameObject kickZone;

	[SerializeField]
	private Vector3 offsetUpper;

	[SerializeField]
	private Sprite jump;

	[SerializeField]
	private Sprite fall;

	[SerializeField]
	private Sprite idle;

	[SerializeField]
	private Vector3 offsetLower;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		shieldPos = new Vector3 (shield.transform.localPosition.x, 0.325f, 0);
		if (StaticPlayer.spawnPosition.z == 0) {
			this.transform.position = StaticPlayer.spawnPosition;
		}
		_mRigidBody = GetComponent<Rigidbody2D> ();
		_moveSpeed = 5f;
		_animator = GetComponent<Animator> ();
		_animator.SetBool ("isCrouching", false);
		healthHandler = GetComponent<PlayerHealthHandler> ();

		kickZone = this.transform.Find ("Kick Zone").gameObject;
		sr = GetComponent<SpriteRenderer> ();
	}
	// Update is called once per frame
	void Update ()
	{
		if (_mRigidBody.velocity.y > 0) {
			_animator.SetBool ("Jumping", true);
			_animator.SetBool ("Falling", false);
		} else if (_mRigidBody.velocity.y < 0) {
			_animator.SetBool ("Falling", true);
			_animator.SetBool ("Jumping", false);
		}
		else{
			_animator.SetBool ("Jumping", false);
			_animator.SetBool ("Falling", false);

		}
		if (!isEnabled)
			return;
		float horizontal = 0;
		if (!_animator.GetBool("isCrouching")) {
			horizontal = Input.GetAxis ("Horizontal");
		}
		if (horizontal < 0.0f && _facingRight == true) {	
			FlipPlayer ();
		} else if (horizontal > 0.0f && _facingRight == false) {
			FlipPlayer ();
		}
		shield.SetActive(!(_animator.GetCurrentAnimatorStateInfo(0).IsName("HighKick") || _animator.GetCurrentAnimatorStateInfo(0).IsName("LowKick")));
		if ((Input.GetKeyDown ("w") || Input.GetKeyDown(KeyCode.UpArrow)) && _mRigidBody.velocity.y == 0) {
			Jump ();
		}
		if ((Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) || _mRigidBody.velocity.y != 0) {
			_animator.SetBool ("isCrouching", false);
			//_animator.StopPlayback ();
			shieldPos = /*Vector3.Lerp(shield.transform.localPosition, */new Vector3 (shield.transform.localPosition.x, 0.325f, 0) /*, 0.5f)*/;
			//_animator.Play("Crouch");
		}
		if ((Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _mRigidBody.velocity.y == 0) {
			_animator.SetBool ("isCrouching", true);
			shieldPos = /*Vector3.Lerp(shield.transform.localPosition, */new Vector3(shield.transform.localPosition.x, -0.485f, 0)/*, 0.5f)*/;
			//_animator.Play("Crouch");
			//Debug.Log ("pressing ss");
		} else if (!_animator.GetBool("isCrouching")) {
			if (_mRigidBody.velocity.y > 0.05f) {
				shieldPos = /*Vector3.Lerp(shield.transform.localPosition, */new Vector3 (shield.transform.localPosition.x, -0.485f, 0)/*, 0.5f)*/;
			} else {
				shieldPos = /*Vector3.Lerp(shield.transform.localPosition, */new Vector3 (shield.transform.localPosition.x, 0.325f, 0)/*, 0.5f)*/;
			}
		}

			

		shield.transform.localPosition = Vector3.Lerp (shield.transform.localPosition, shieldPos, 0.15f);

		if(_facingRight)
		{
			offsetUpper.x = 1.8F;
			offsetLower.x = 1.8F;
		}else
		{
			offsetUpper.x = -1.8F;
			offsetLower.x = -1.8F;
		}

		if (_animator.GetBool ("isCrouching")) {
			kickZone.transform.position = transform.position + offsetLower;
			//healthHandler.curHeight = PlayerHealthHandler.attackHeight.Low;
		}else{
			kickZone.transform.position = transform.position + offsetUpper;
			//healthHandler.curHeight = PlayerHealthHandler.attackHeight.High;
		}
		kick ();
		handleBullet ();
		//Debug.Log (horizontal);
		_animator.SetFloat ("Speed", Math.Abs(horizontal));
		HandleMovement (horizontal);
	}

	private void Jump(){
		_mRigidBody.AddForce (Vector2.up * _playerJumpPower);
	}

	private void FlipPlayer(){
		_facingRight = !_facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	private void HandleMovement(float horizontal){
		_mRigidBody.velocity = new Vector2 (horizontal*_moveSpeed, _mRigidBody.velocity.y);
	}

	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			healthHandler.takeDamage (/*0.5f/*,col.gameObject.GetComponent<Enemy>().AH*/);
		} else if (col.gameObject.layer == LayerMask.NameToLayer ("Fairy")) {
			StaticPlayer.setHealth (Config.MAX_HEALTH);
			DestroyObject (col.gameObject);
		}
		//Debug.Log (StaticPlayer.getHealth ());
	}

	void handleBullet()
	{
		if (_projectiles.Count == 0 /*&& (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") ||_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) */ && StaticPlayer.getHealth() >= Config.MAX_HEALTH) {
			if (Input.GetKeyDown ("space") && !healthHandler.immune) {
				_animator.Play("HighKick");
				GameObject bullet = (GameObject)Instantiate (_projectilePrefab, transform.position, Quaternion.identity);
				bullet.GetComponent<BulletForce> ().setOrientation (_facingRight);
				//we are crouching so lower the bullet
				if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) {
					bullet.transform.position += new Vector3 (0, -0.7f, 0);
					_animator.Play ("LowKick");
				} else {
					_animator.Play("HighKick");
				}
				_projectiles.Add (bullet);
				_bulletCounter.Add (1);
				AudioSource audio = GetComponent<AudioSource> ();
				audio.Play ();
				if (_facingRight) {
					bulletMovingRight = true;
				} else {
					bulletMovingRight = false;
				}
			}
		}

		for (int i = 0; i < _projectiles.Count; i++) {
			GameObject currentBullet = _projectiles [i];
			if (currentBullet != null) {
				_bulletCounter [i] += 1;
				if (bulletMovingRight) {
					currentBullet.transform.Translate (new Vector3 (1, 0) * Time.deltaTime * projectileVelocity);
				} else {
					currentBullet.transform.Translate (new Vector3 (1, 0) * Time.deltaTime * projectileVelocity * -1);
				}
				if (_bulletCounter[i] == 15) {
					_projectiles.Remove (currentBullet);
					_bulletCounter.RemoveAt (i);
					DestroyObject (currentBullet);
				}
			}
		}
	}

	void kick()
	{
		if(Input.GetKeyDown("space"))
		{
			StartCoroutine (delayDeactivate ());
		}
	}
	IEnumerator delayDeactivate()
	{
		kickZone.SetActive (true);
		yield return new WaitForSeconds (_kickZoneDeactDelay);
		kickZone.SetActive (false);
		yield return null;
	}
}