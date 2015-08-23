using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public const int SHOOT_PLAYER = 0;
	public const int JUMP_PLAYER = 1;
	public const int DIE_PLAYER = 2;
	public const int SHOOT_ENEMY = 3;
	public const int DIE_ENEMY = 4;
	public const int AMMO_EXPLOSION = 5;
	public const int SELECT = 6;
	public const int BACKGROUND = 7;

	AudioSource[] audios;
	public bool backPlaying = false;


	void Start () {
		DontDestroyOnLoad (this.gameObject);

		audios = GetComponents<AudioSource> ();
	}

	public void Play(int sound) {
		audios [sound].Play ();
	}

	public void PlayBackgroundSound() {
		if (!backPlaying) {
			Play (7);
			backPlaying = true;
		}
	}

}