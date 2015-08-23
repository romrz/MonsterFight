using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Enemy : MonoBehaviour {
	
	public float gravity = -5;
	public float moveSpeed = 1;
	public float shootTimeInSight = 0.8f;
	public float shootTimeNormal = 5;
	public Vector2 shootVelocity = new Vector2(-11, 0);

	float shootTime;
	float elapsedShootTime = 0;

	float minMovementX, maxMovementX;
	public int direction;

	public GameObject bullet;
	
	Controller2D controller;
	AudioSource audioSource;

	bool playerInSight;
	Vector3 velocity;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
		controller = GetComponent<Controller2D> ();
		shootTime = shootTimeNormal;
		direction = (Random.Range (1, 11) > 5 ? 1 : -1);
	}

	void Update() {
		elapsedShootTime += Time.deltaTime;

		if (playerInSight)
			shootTime = shootTimeInSight;
		else
			shootTime = shootTimeNormal;

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		if (transform.position.x > maxMovementX) {
			direction *= -1;
			transform.position = new Vector3(maxMovementX, transform.position.y, transform.position.z);
		}
		if (transform.position.x < minMovementX) {
			direction *= -1;
			transform.position = new Vector3(minMovementX, transform.position.y, transform.position.z);
		}

		velocity.x = moveSpeed * direction;
		velocity.y += gravity * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime);

		if (elapsedShootTime >= shootTime) {
			elapsedShootTime -= shootTime;
			Shoot();
		}

		playerInSight = false;

		if(direction == -1 && transform.localScale.x < 0)
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
		if(direction == 1 && transform.localScale.x > 0)
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
	}

	public void SetMovement(float min, float max) {
		minMovementX = transform.position.x - min;
		maxMovementX = transform.position.x + max;
	}

	public int GetDirection() {
		return direction;
	}

	public void Shoot() {
		GameObject b = (GameObject) Instantiate (
			bullet,
			new Vector3(transform.position.x, transform.position.y, -1),
			Quaternion.identity);
		
		b.GetComponent<Ammo> ().velocity = new Vector2(
			(Mathf.Abs (velocity.x) + shootVelocity.x) * direction, 0);
	
		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (3);
	}

	public void Hit() {
		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (4);
		Destroy (this.gameObject);
	}

	public void PlayerInSight() {
		playerInSight = true;
	}
}