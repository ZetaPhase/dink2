using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.CodeDom.Compiler;

[System.Serializable]
public class DialougeNode
{
    // A single side of the dialouge.. string these together to create a full converstation
	[Header("Chat information")]
	[TextArea]
	public string Dialouge;
}

public class Dialouge : MonoBehaviour 
{
    // Handles all the dialouge for the game... enjoy reading it!
	private bool inChat;
	private int lineNum;
	public List<DialougeNode> dNode;

	[SerializeField]
	private GameObject SubtitleHub;
	[SerializeField]
	private Text SubtitleText;
	[SerializeField]
	private GameObject PlayerHud;
	[SerializeField]
	private Movement M;

	void Start()
	{
	}

	void Update ()
	{
		if (inChat) {
			SubtitleHub.SetActive (true);
			PlayerHud.SetActive (false);

			M.isEnabled = false;
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (lineNum >= dNode.Count - 1) {
					// if the linenumber is not smaller than the current number of nodes then you are not in chat
					inChat = false;
				} else {
					lineNum++;
				}
				updateDialouge ();
				Time.timeScale = .01F;
			} 
		}else {
			SubtitleHub.SetActive (false);
			PlayerHud.SetActive (true);
			Time.timeScale = 1F;
			M.isEnabled = true;
		}
	
	}
	void updateDialouge()
	{
		SubtitleText.text = dNode [lineNum].Dialouge;
	}
	public void ChangeDialouge(List<DialougeNode> NewNodes)
	{
        // A function to change the Dialouge outright
		dNode.Clear ();
		dNode.AddRange (NewNodes);

		lineNum = 0;
		inChat = true;
		updateDialouge ();
	}
}
