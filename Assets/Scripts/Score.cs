using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public Player player;

	void Update () {
		this.GetComponent<Text>().text = player.GetScore ().ToString();
	}
}

