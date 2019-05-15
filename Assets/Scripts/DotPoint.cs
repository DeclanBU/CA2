using UnityEngine;
using System.Collections;
//This script controls the rotation of blue point.
public class DotPoint : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
		//rotating the blue point on its z axis.
		transform.Rotate (0, 0, 50 * Time.deltaTime);
	}

}
