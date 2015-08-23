using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {

	public Controller2D player;

	void Update () {
			transform.position = new Vector3 (
			player.transform.position.x,	
			transform.position.y,
			transform.position.z);
	}
}
