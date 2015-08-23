using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Update () {
		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().PlayBackgroundSound ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (6);
	
			Application.LoadLevel("GameScene");
		}
	}
}