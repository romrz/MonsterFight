using UnityEngine;
using System.Collections;

public class GameRestart : MonoBehaviour
{
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (6);
			
			Application.LoadLevel("MainMenuScene");
		}
	}
}