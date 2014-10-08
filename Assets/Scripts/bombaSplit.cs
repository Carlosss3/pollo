using UnityEngine;
using System.Collections;

public class bombaSplit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("se ha chocado con la bomba");
	}
}

