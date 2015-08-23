using UnityEngine;
using System.Collections;

public class PlatformController : RaycastController {

	void Update() {
		UpdateRaycastOrigins ();

		HorizontalCollisions (-1);
	}

	void HorizontalCollisions(int direction) {
		float rayLength = skinWidth;
		
		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (direction == 1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * direction, rayLength, collisionMask);
			
			if(hit) {
				if(hit.collider.tag == "PlatformDestroyer") {
					Destroy (this.gameObject);
				}
			}
		}
	}

}