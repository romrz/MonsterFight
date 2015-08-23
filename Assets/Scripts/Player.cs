using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (SightController))]
public class Player : MonoBehaviour {

	//public GameObject scoreManagerObject;

	public float jumpHeight = 3;
	public float timeToJump = 0.3f;
	public float moveSpeed = 5;

	float jumpVelocity;
	float gravity;
	Vector3 velocity;
	int direction = 1;

	public GameObject ammo;
	Vector2 shootVelocity = new Vector2(11, 0);

	Controller2D controller;
	SightController sightController;

	Animator animator;
	AudioSource audioSource;

	int score = 0;

	float backLimit;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		controller = GetComponent<Controller2D> ();
		sightController = GetComponent<SightController> ();

		animator = GetComponent<Animator> ();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJump, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJump;

		backLimit = transform.position.x - 10f;

		//GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ().score = score;
	}
	
	void Update () {
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		if (controller.collisions.left || controller.collisions.right) {
			velocity.x = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		direction = (int) Mathf.Sign(input.x);

		if (Input.GetKeyDown (KeyCode.UpArrow) && controller.collisions.below) {
			velocity.y = jumpVelocity;
			GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (1);
		}

		velocity.x = input.x * moveSpeed;
		velocity.y += gravity * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime, input);
		sightController.Sight ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}

		if(direction == -1 && transform.localScale.x > 0)
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
		if(direction == 1 && transform.localScale.x < 0)
			transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
		

		animator.SetBool ("OnGround", controller.collisions.below);
		animator.SetBool ("Standing", controller.collisions.below && velocity.x == 0);
		animator.SetFloat ("VelocityY", velocity.y);
		animator.SetFloat ("VelocityX", Mathf.Abs (velocity.x));
	}

	void Shoot() {
		GameObject _ammo = (GameObject) Instantiate (
			ammo,
			new Vector3(transform.position.x, transform.position.y + 0.2f, -1),
			Quaternion.identity);

		_ammo.GetComponent<Ammo> ().velocity = new Vector2(
			(Mathf.Abs (velocity.x) + shootVelocity.x) * direction, 0);

		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (0);
	}

	public void AddPointScore() {
		score++;
		GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager> ().score = score;
	}

	public int GetScore() {
		return score;
	}

	public void Hit() {
		Die ();
	}

	public void Die() {
		GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().Play (2);
		Application.LoadLevel ("GameOverScene");
	}

}