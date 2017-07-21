using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatInitlaizer : MonoBehaviour {
    // Used for initializing a chat conversation
	[SerializeField]
	private Dialouge dialouge;
	[SerializeField]
	private bool isRepeatable;
	[SerializeField]
	private bool canChat;
	[SerializeField]
	private List<DialougeNode> dNodes;

	private bool triggered;

	void OnTriggerEnter2D (Collider2D Col) 
	{
		if(Col.tag == "Player" && canChat == true)
		{
			triggered = true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.tag == "Player" && canChat == true)
		{
			triggered = false;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.C) && triggered == true) {
			dialouge.ChangeDialouge (dNodes);
			if (!isRepeatable) {
				canChat = false;
			} 
		}
	}
}
