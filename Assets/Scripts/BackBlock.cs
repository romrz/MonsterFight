using UnityEngine;
using System.Collections;

public class BackBlock : MonoBehaviour
{
	public Controller2D controller;
	public float separation;
	float lastPos;

	// Use this for initialization
	void Start ()
	{
		lastPos = controller.transform.position.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (lastPos < controller.transform.position.x) {
			transform.position = new Vector3(controller.transform.position.x - separation, transform.position.y, transform.position.z);
			lastPos = transform.position.x;
		}
	}
}

