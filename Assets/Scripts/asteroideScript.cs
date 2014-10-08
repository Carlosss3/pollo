using UnityEngine;
using System.Collections;

public class asteroideScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	  if (Input.GetKey ("t")) {
						rigidbody2D.gravityScale = 1;

				}
		//convierto el parametro gravityScale en 1 al pulsar la tecla t
	
		if (Input.GetKey ("r")) {
			rigidbody2D.gravityScale = 0;
			rigidbody2D.AddForce (new Vector2 (0,10));
		}
		//Para terminar
	}

}
