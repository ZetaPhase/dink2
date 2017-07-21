using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	//public PlayerHealthHandler.attackHeight AH;
	//public float xpEarned;
	[SerializeField]
	private float maxHealth;

	private float curHealth;

	[SerializeField]
	private Image healthFill;

	void Start()
	{
		curHealth = maxHealth;
	}

	public void takeDamage(float damage)
	{
		//this.GetComponent<AudioSource> ().Play ();
		curHealth -= damage;
		updateUI ();

		if (curHealth <= 0F)
			Destroy (this.gameObject);
	}

	void updateUI()
	{
		healthFill.fillAmount = curHealth / maxHealth;
	}
}
