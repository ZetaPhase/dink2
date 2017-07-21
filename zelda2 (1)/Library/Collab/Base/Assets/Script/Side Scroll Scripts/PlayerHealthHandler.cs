using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour {

	[SerializeField]
	private float _playerHealth;

	[SerializeField]
	private Image healthBar;

	void Start()
	{
		_playerHealth = StaticPlayer.getHealth();
		updateUI ();
	}

	public void takeDamage(float damage)
	{
		StaticPlayer.setHealth (StaticPlayer.getHealth()-damage);
	}

	void checkHealth()
	{
		updateUI ();
		if(_playerHealth <= 0)
		{
			MainMenuControl.backToMainMenu ();
		}
	}
	void updateUI()
	{
		healthBar.fillAmount = (_playerHealth / Config.MAX_HEALTH);
	}
}