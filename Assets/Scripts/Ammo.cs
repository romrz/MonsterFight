using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AmmoController))]
public class Ammo : MonoBehaviour {

	public Vector2 velocity = new Vector2(0, 0);

	AmmoController controller;

	void Start() {
		controller = GetComponent<AmmoController> ();
	}

	void Update () {
		controller.Move (velocity * Time.deltaTime);
	}
}