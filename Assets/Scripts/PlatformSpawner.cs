using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	public GameObject[] platforms;
	public float platformSeparation = 2;
	public GameObject enemy;
	public bool FallThroughPlatform = true;
	public int enemySpawnProbability = 80;

	float nextSpawn;

	void Start ()
	{
		float horizontalCameraExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		float startX = -horizontalCameraExtent;
		float endX = transform.position.x;
		float currentX = startX;

		while (currentX <= endX) {
			GameObject platform = Spawn(
				platforms[Random.Range (0, platforms.Length)],
				new Vector3(currentX, transform.position.y, 0));

			float x = platform.GetComponent<BoxCollider2D>().bounds.size.x + platformSeparation;
			currentX += x;
		}

		nextSpawn = currentX;
	}
	
	void Update ()
	{
		if (transform.position.x >= nextSpawn) {
			GameObject platform = Spawn(
				platforms[Random.Range (0, platforms.Length)],
				new Vector3(nextSpawn, transform.position.y, 0));
			
			float x = platform.GetComponent<BoxCollider2D>().bounds.size.x + platformSeparation;
			nextSpawn += x;
		}
	}

	GameObject Spawn(GameObject obj, Vector3 pos) {
		GameObject platform = (GameObject) Instantiate(obj, pos, Quaternion.identity);
		Bounds platformBounds = platform.GetComponent<BoxCollider2D> ().bounds;
		//platform.transform.Translate (Vector3.right * platform.GetComponent<Renderer> ().bounds.extents.x);

		if(!FallThroughPlatform)
			platform.tag = "Platform";

		if (Random.Range (0, 100) < enemySpawnProbability) {

			GameObject _enemy = (GameObject)Instantiate (
			enemy,
			new Vector3 (platformBounds.center.x, platformBounds.center.y + 2, -2),
			Quaternion.identity);
			Vector2 enemyExtents = _enemy.GetComponent<Renderer> ().bounds.extents;

			//Debug.DrawLine (platformBounds.min, platformBounds.max, Color.red, 10);

			float platformExtentsX = (platformBounds.max.x - platformBounds.min.x) / 2.0f;

			_enemy.GetComponent <Enemy> ().SetMovement (
			platformBounds.extents.x - enemyExtents.x,  //platform.transform.position.x - platformExtentsX + enemyExtents.x,
			platformBounds.extents.x - enemyExtents.x); //platform.transform.position.x + platformExtentsX - enemyExtents.x);

		}
		return platform;
	}

}