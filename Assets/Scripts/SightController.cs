using UnityEngine;
using System.Collections;

public class SightController : RaycastController {

	public float rayLength = 20;

	public void Sight() {
		UpdateRaycastOrigins ();
		HorizontalSight (1);
		HorizontalSight (-1);
	}

	void HorizontalSight(int direction) {
		float directionX = direction;
		
		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == 1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
			
			if(hit) {
				if(hit.collider.tag == "Enemy") {
					Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
					if(enemy.GetDirection() == -direction)
						enemy.PlayerInSight();
				}
			}
			
			if(inDebug)
				Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);
		}
	}
	
}