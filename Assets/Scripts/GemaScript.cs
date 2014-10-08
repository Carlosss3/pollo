using UnityEngine;
using System.Collections;

public class GemaScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Player")
				Application.LoadLevel ("Nivel 01");

		Debug.Log ("Tocando Gema");
		}

}
