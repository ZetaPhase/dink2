using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthHandler : MonoBehaviour {

	//public enum attackHeight {High, Low};
	//public attackHeight curHeight;

	//[SerializeField]
	//private float StaticPlayer.health;

	//[SerializeField]
	private Image healthBar;
	public bool immune;
	//[SerializeField]
	//private GameObject sheild;
	[SerializeField]
	private AudioClip damage;
	void Start()
	{
		StaticPlayer.health = StaticPlayer.getHealth();
		healthBar = GameObject.FindGameObjectWithTag ("Hp Bar").GetComponent<Image> ();
		updateUI ();
	}
	public void FullHeal ()
	{
		StaticPlayer.health = Config.MAX_HEALTH;
		updateUI ();
		checkHealth ();
	}
	public void takeDamage(/*float damage /*,attackHeight AH*/)
	{
		//if (sheild.activeInHierarchy == true && AH == curHeight/* && sheild.GetComponent<SheildHealth>().isAbleToBlock*/) {
			//sheild.GetComponent<SheildHealth> ().usesLeft--;
		//}else
		//{
		if (immune) {
			//Debug.Log ("immunity");
			return;
		}
		this.GetComponent<AudioSource> ().clip = damage;
		this.GetComponent<AudioSource> ().Play ();
		StartCoroutine ("immunity");
		StaticPlayer.health -= 1; //damage;
			//StaticPlayer.setHealth (StaticPlayer.getHealth()-damage);
			updateUI ();
		checkHealth ();
		//}
	}
	void checkHealth()
	{
		updateUI ();
		if(StaticPlayer.health <= 0)
		{
			StaticPlayer.lives--;
			if (StaticPlayer.lives <= 0) {
				StaticPlayer.health = Config.MAX_HEALTH;
				StaticPlayer.resetLiveCounter ();
				SceneManager.LoadScene ("GameOver");
			} else {
				StaticPlayer.health = Config.MAX_HEALTH;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}
	}
	void updateUI()
	{
		healthBar.fillAmount = (StaticPlayer.health / Config.MAX_HEALTH);
		if(StaticPlayer.health <= Config.MAX_HEALTH /4F)
		{
			healthBar.color = Color.red;
		}else{
			healthBar.color = Color.green;
		}
	}
	IEnumerator immunity ()
	{
		immune = true;
		this.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.25f);
		this.GetComponent<SpriteRenderer> ().color = Color.white;
		immune = false;
		yield return null;
		
	}
}