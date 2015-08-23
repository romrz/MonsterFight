using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score;

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

}