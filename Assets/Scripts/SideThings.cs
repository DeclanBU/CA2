using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideThings : MonoBehaviour {

	public Sprite lilipads;
	public Sprite bugs;
	public Sprite crabs;
	public Sprite worms;
	public Sprite snail;

	private Sprite currentSp;

	// Use this for initialization
	void Start () {
		int val = PlayerPrefs.GetInt ("counter", 0);

		if(val == 0){
			currentSp = snail;
		}
		else if(val == 1){
			currentSp = worms;
		}
		else if(val == 2){
			currentSp = bugs;
		}
		else if(val == 3){
			currentSp = crabs;
		}
		else if(val == 4){
			currentSp = lilipads;
		}

		for(int i = 0; i < transform.childCount; i++){
			transform.GetChild (i).GetComponent<SpriteRenderer> ().sprite = currentSp;

//			if (val == 0) {
//				if (i < 5) {
//					transform.GetChild (i).gameObject.SetActive (false);
//				}
//			} else {
//				if (i < 5) {
//					transform.GetChild (i).gameObject.SetActive (true);
//				}
//			}
		}

	}
}
