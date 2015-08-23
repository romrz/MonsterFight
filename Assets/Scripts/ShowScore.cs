using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScore : MonoBehaviour
{

	void Start () {
		ScoreManager scoreManager = GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ();
		GetComponent<Text> ().text = scoreManager.score.ToString();
		scoreManager.score = 0;
	}

}