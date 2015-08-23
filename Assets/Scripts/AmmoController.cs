using UnityEngine;
using System.Collections;

public class AmmoController : RaycastController {

	public void Move(Vector3 distance) {
		UpdateRaycastOrigins ();

		if(distance.x != 0)
			HorizontalCollisions (ref distance);
		
		transform.Translate (distance);
	}
	
	void HorizontalCollisions(ref Vector3 distance) {
		float directionX = Mathf.Sign (distance.x);
		float rayLength = Mathf.Abs (distance.x) + skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == 1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			
			if(hit) {
				if(hit.collider.tag == "Enemy") {
					hit.collider.gameObject.GetComponent<Enemy>().Hit();
					Destroy (this.gameObject);

					GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddPointScore();
				}
				if(hit.collider.tag == "Player") {
					hit.collider.gameObject.GetComponent<Player>().Hit();
					Destroy (this.gameObject);
				}
				if(hit.collider.tag == "BulletDestroyer") {
					Destroy (this.gameObject);
				}
				if(hit.collider.tag == "PlatformDestroyer") {
					Destroy (this.gameObject);
				}
				if(hit.collider.tag == "Ammo") {
					GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (5);
					
					Destroy (this.gameObject);
					Destroy (hit.collider.gameObject);
				}
			}
			
			if(inDebug)
				Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);
		}
	}

}