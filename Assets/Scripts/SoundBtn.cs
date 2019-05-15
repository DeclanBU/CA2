using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtn : MonoBehaviour
{
	public Sprite soundOn;
	public Sprite soundOff;

	public AudioSource music;

	// Start is called before the first frame update
	void Start()
	{
		//music = Camera.main.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void onSoundBtn(){

		if (music.mute) {
			GetComponent<Image> ().sprite = soundOn;
			music.mute = false;
		} else {
			GetComponent<Image> ().sprite = soundOff;
			music.mute = true;
		}
	}
}
