using UnityEngine;
using System.Collections;
//This script controls the movement of enemy black dot.
public class DotEnemy : MonoBehaviour {

	string direction;
	[Range(0.0f, 10.0f)] public float speed;
	// Use this for initialization
	void Start () {
		//It checks where the black dot is instantiated and assigns direction to it.
		if (transform.position.y > 5) {
			direction = "down";
		}
		if (transform.position.y < -5) {
			direction = "up";
		}
		if (transform.position.x < -5) {
			direction = "right";
		}
		if (transform.position.x > 5) {
			direction = "left";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameController.isGameOver) {
			//It sets the movement according to the direction.
			if (direction == "down") {
				transform.Translate (Vector2.down * Time.deltaTime * speed);
			}
			if (direction == "up") {
				transform.Translate (Vector2.up * Time.deltaTime * speed);
			}
			if (direction == "right") {
				transform.Translate (Vector2.right * Time.deltaTime * speed);
			}
			if (direction == "left") {
				transform.Translate (Vector2.left * Time.deltaTime * speed);
			}

			//Destroy the black dot after it leaves the gameplay area.
			if ((transform.position.x > 15) || (transform.position.x < -15) || (transform.position.y > 15) || (transform.position.y < -15)) {
				Destroy (this.gameObject);
			}
		}
	}
}
