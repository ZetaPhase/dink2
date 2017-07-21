using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

	public void startGame()
	{
		SceneManager.LoadScene ("DarkenedEarth");
	}
	public void restartGame()
	{
		SceneManager.LoadScene ("MainMenu");
	}
	public void exitGame()
	{
		Application.Quit ();
	}
	public static void backToMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
