using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController {

	Vector2 playerInput;

	[HideInInspector]
	public CollisionInfo collisions;

	public void Move (Vector3 distance) {
		Move (distance, new Vector2(0, 0));
	}

	public void Move(Vector3 distance, Vector2 input) {
		UpdateRaycastOrigins ();
		collisions.Reset ();

		playerInput = input;

		if(distance.x != 0)
			HorizontalCollisions (ref distance);
		if(distance.y != 0)
			VerticalCollisions (ref distance);

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
				if(hit.distance == 0)
					continue;

				//distance.x = (hit.distance - skinWidth) * directionX;
				//rayLength = hit.distance;

				collisions.left = (directionX == -1);
				collisions.right = (directionX == 1);
			}

			if(inDebug)
				Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLength, Color.red);
		}
	}

	void VerticalCollisions(ref Vector3 distance) {
		float directionY = Mathf.Sign (distance.y);
		float rayLength = Mathf.Abs (distance.y) + skinWidth;
		
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == 1) ? raycastOrigins.topLeft : raycastOrigins.bottomLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i);

			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
			
			if(hit) {
				if(hit.collider.tag == "ThroughPlatform") {
					if(directionY == 1 || hit.distance == 0)
						continue;
					if(playerInput.y == -1)
						continue;
				}

				if(hit.collider.tag == "DestroyerBottom") {
					if(this.gameObject.tag == "Enemy") {
						Destroy (this.gameObject);
					}
					if(this.gameObject.tag == "Player") {
						GetComponent<Player>().Die();
					}
				}

				if(hit.collider.tag == "PlatformDestroyer") {
					if(this.gameObject.tag == "Enemy") {
						Destroy (this.gameObject);
					}
				}

				distance.y = (hit.distance - skinWidth) * directionY;
				rayLength = hit.distance;

				collisions.below = (directionY == -1);
				collisions.above = (directionY == 1);
			}

			if(inDebug)
				Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);
		}
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
		public bool fallingThroughPlatform;

		public void Reset() {
			above = below = false;
			left = right = false;
			fallingThroughPlatform = false;
		}
	}

}
