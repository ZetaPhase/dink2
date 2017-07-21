using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossBehavior : MonoBehaviour {

	private Vector3 shieldPos;
	[SerializeField]
	private GameObject shield;
	[SerializeField]
	private float _maxHealth;

	private float _curHealth;

	private Rigidbody2D _rb;

	private float _moveSpeed = 5F;
	private float _jumpPower = 200F;

	private bool _facingRight;

	//0 = moveRight, 1=moveLeft,2=jump
	private int chosenMoveMode;

	//0 = Magic, 1 = Melee
	private int chosenAttackMode;

	private float timeBetweenChangesMoving = .7F;

	private float timeBetweenChangesAttacking = .2F;

	[SerializeField]
	private Vector3 offsetUpper;

	[SerializeField]
	private Vector3 offsetLower;

	private float _kickZoneDeactDelay = .1F;

	[SerializeField]
	private GameObject kickZone;

	[SerializeField]
	private GameObject _projectilePrefab;

	private List<GameObject> _projectiles = new List<GameObject> ();

	private Animator _animator;

	[SerializeField]
	private Image healthBar;

	// Use this for initialization
	void Start () {
		shieldPos = new Vector3 (shield.transform.localPosition.x, 0.325f, 0);
		_curHealth = _maxHealth;
		_rb = GetComponent<Rigidbody2D> ();
		StartCoroutine (waitAndChangeMoveModes ());
		//kickZone = this.transform.Find ("Kick Zone").gameObject;
		_animator = GetComponent<Animator> ();
		_animator.SetBool ("isCrouching", false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(chosenMoveMode == 0)
		{
			HandleMovement (1F);
			if(_facingRight == false)
				FlipPlayer ();
		}else if(chosenMoveMode == 1)
		{
			HandleMovement (-1F);
			if(_facingRight == true)
				FlipPlayer();
		}else if(chosenMoveMode == 2 || chosenMoveMode == 3)
		{
			Jump();
		}else if(chosenMoveMode == 4 || chosenMoveMode == 5)
		{
			crouch ();
		}

		if(chosenAttackMode == 0 && _curHealth >= _maxHealth -2) 
		{
			handleBullet ();
		}else
		{
			kick ();
		}

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

		if (_rb.velocity.y != 0) {
			_animator.SetBool ("isCrouching", false);
		}

		if (_rb.velocity.y == 0) {
			_animator.SetBool ("isCrouching", true);
			shieldPos =new Vector3 (shield.transform.localPosition.x, -0.485f, 0);
		} else if (!_animator.GetBool ("isCrouching")) {
			if (_rb.velocity.y > 0.05f) {
				shieldPos = new Vector3 (shield.transform.localPosition.x, -0.485f, 0);
			} else {
				shieldPos =new Vector3 (shield.transform.localPosition.x, 0.325f, 0);
			}
		}
		shield.transform.localPosition = Vector3.Lerp (shield.transform.localPosition, shieldPos, 0.5f);
	}

	private void Jump(){
		if(_rb.velocity.y == 0)
			_rb.AddForce (Vector2.up * _jumpPower);
	}

	private void FlipPlayer(){
		_facingRight = !_facingRight;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	private void HandleMovement(float horizontal){
		_animator.SetFloat ("Speed", 1F);
		_rb.velocity = new Vector2 (horizontal*_moveSpeed, _rb.velocity.y);
	}

	IEnumerator waitAndChangeMoveModes()
	{
		chosenMoveMode = Random.Range (0, 5);
		yield return new WaitForSeconds (timeBetweenChangesMoving);
		StartCoroutine (waitAndChangeMoveModes ());
	}

	IEnumerator waitAndChangeAttackModes()
	{
		chosenAttackMode = Random.Range (0, 5);
		yield return new WaitForSeconds (timeBetweenChangesAttacking);
		StartCoroutine (waitAndChangeAttackModes ());
	}

	void handleBullet()
	{
		if (_projectiles.Count == 0 /*&& (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle") ||_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) */ ) {
			if (Vector3.Distance (transform.position, GameObject.FindWithTag ("Player").transform.position) > 2F)
				return;
			GameObject bullet = (GameObject)Instantiate (_projectilePrefab, transform.position, Quaternion.identity);

			bullet.GetComponent<BulletForce> ().setOrientation (_facingRight);

			//we are crouching so lower the bullet
			if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) {
				bullet.transform.position += new Vector3 (0, -0.7f, 0);
			}
			_projectiles.Add (bullet);
			AudioSource audio = GetComponent<AudioSource> ();

			audio.Play ();
		}

		for (int i = 0; i < _projectiles.Count; i++) {
			GameObject currentBullet = _projectiles [i];
			if (currentBullet == null) 
				_projectiles.RemoveAt (i);
		}
	}

	void kick()
	{
		StartCoroutine (delayDeactivate ());
		if (_animator.GetCurrentAnimatorStateInfo (0).IsName ("Crouch")) {
			_animator.Play ("LowKick");
		} else if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("LowKick") && !_animator.GetCurrentAnimatorStateInfo (0).IsName ("HighKick")){
			_animator.Play ("HighKick");
		}
	}
	IEnumerator delayDeactivate()
	{
		kickZone.SetActive (true);
		yield return new WaitForSeconds (_kickZoneDeactDelay);
		kickZone.SetActive (false);
		yield return null;
	}

	void crouch()
	{
		if(_rb.velocity.y == 0f)
		{
			_rb.velocity = Vector2.zero;
			_animator.SetBool ("isCrouching", true);
		}
	}
	public void takeDamage(float damage)
	{
		_curHealth -= damage;
		updateUI ();
		if (_curHealth <= 0F) {
			Destroy (this.gameObject);
			SceneManager.LoadScene ("WinScene");
		}
			
	}

	void updateUI()
	{
		healthBar.fillAmount = _curHealth / _maxHealth;
	}
}
