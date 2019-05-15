using UnityEngine;
using System.Collections;
//This script controls the movement of the plusone sign and also destroys it.
public class PlusOne : MonoBehaviour {

	GameObject player;
	float verticalValue;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("DotPlayer");
		verticalValue = player.GetComponent<GameController> ().verticalValue;
		//destroying the plusone sign after half second. 
		Destroy (this.gameObject, 0.5f);
	}

	void Update(){
	//moving the sign slowly in vertical direction.
		transform.Translate (new Vector2 (0, 1* verticalValue) * Time.deltaTime*2);
	}
}
