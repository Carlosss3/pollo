
using UnityEngine;
using System.Collections;

public class jumpScript : MonoBehaviour {
	public int jumpForce = 200; //esto es el salto
	public int moveForce = 50; //Fuerza horizontal al mover
	public int maxSpeed = 50; //Velocidad maxima horizontal
	public AudioClip sonidoVolar;
	public AudioClip sonidoHerido;
	public AudioClip sonidoCurado;

	private bool estaherido = false;
	public bool tocando_suelo = true;

	public float escala = 0.5f;



	//Con private bool estaherido = false lo que hago es decir que el pollo por defecto no estara herido

	//Esto es necesario para poder manejar las animaciones
	Animator animacion; 
	// a la palabra animacion le doy la responsabilidad de Animator

	// Use this for initialization
	void Start () {
		//Al iniciar cargamos las variables de las animaciones.
		animacion = GetComponent <Animator> ();
	   //Get Component, te mete el componente Animator en una variable. En nuestro caso, en la variable animacion.
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Jump")) { // Cuando pulsamos "espacio" salta

						saltar (); //Mas abajo esta la funcion explicada
				}


		if (Input.GetKey ("a")) { //Mover a la izquierda
						mover (moveForce * -1); //Multiplicamos negativo para ir a la izquierda.
			transform.localScale = new Vector3(-1*escala,escala,escala); // Esto quiere decir que nos variara la escala a -1, que hace que el muñeco este al reves)
		}
		
		if (Input.GetKey ("d")) { //Mover a la derecha
						mover (moveForce);
			transform.localScale = new Vector3(escala,escala,escala);
				}
	}
	/* Funcion saltar
	 * Esta funcion aplica una fuerza hacia arriba definida por la variablejumpForce
	 * TODO: Falta animacion de salto y sonido. 
	 */

	void saltar() {
		//Aplicamos una fuerza con rigidbody2d.AddForce . Al añadir rigidbody2d hago referencia a los parametros del "preset" aplicado para que caiga.
		// newVector2(0,jumpForce) es un vector con la X a cero y la Y a jumpForce
		if(!estaherido & tocando_suelo){
			//El signo de exclamacion significa "no". de manera que lo que stoy diciendo es, que si el pollo no esta herido... todo lo de abajo)
			rigidbody2D.AddForce (new Vector2 (0, jumpForce));
			tocando_suelo = false;}
		//al parametro rigidbody2d le añado una fuerza en direccion Y cuando pulse espacio. Eso es lo que pone.
		animacion.SetBool ("Volando", true);
		AudioSource.PlayClipAtPoint (sonidoVolar, transform.position);

    }   //hacemos que el sonido suene en la posicion del pollo.




	/*Funcion Mover
	 * Parametros: fuerza -> Fuerza que le vamos a aplicar para moverse. 
	 * Para mover tengo que especificar en Update que mover (moveforce) porque tiene dos direcciones mientras que en saltar no.
	 * Aplicamos una fuerza horizontal teniendo en cuenta no sobrepasar la velocidad maxima.
	 */
	void mover(int fuerza){
		// Creamos una variable para guardar la velocidad acutal
		//float es un numero con decimales. y velocity es = rigidbody2D.velocity.x;
		float velocity = rigidbody2D.velocity.x;
		//Math.Abs siempre me devuelve el valor absoluto, (numero en positivo) para calcular la velocidad maxima
		//Si hacemos Mathf.Abs (-10) nos devuelve 10, si es athf.Abs (-30) nos devuelve 30

		if ((fuerza > 0 & velocity < 0) || (fuerza < 0 & velocity > 0)) {
				rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		}

		if(Mathf.Abs(velocity) < maxSpeed)
			//Si la velocidad absoluta es menos de maxSpeed aplicamos una fuerza horizontal.
			rigidbody2D.AddForce(Vector2.right * fuerza);
}

	void damage() {
		estaherido = true;
		animacion.SetBool ("Damage", true);
		AudioSource.PlayClipAtPoint (sonidoHerido, transform.position);
	  
	}

	void OnCollisionEnter2D(Collision2D coll) {
		animacion.SetBool ("Volando", false);
		if (coll.gameObject.tag == "Enemy")
		damage ();
	//Cuando el pajaro choque con algo, para de volar. "El parametro boleano animacion, cuando collision con algo, volando es falso.
    /*coll, se llama asi porque he querido ponerle ese nombre, podria tener otro nombre. Conl o cual la frase quiere decir: si colisiono con un objeto con el game
     * object, tag. que sea Enemigo, se lanza la funcion Damage, la cual esta arriba.
     */
	
		if (coll.gameObject.tag == "Sombrero")
						curar ();

		if (coll.gameObject.tag == "Plataforma")
				tocando_suelo = true;
						
	}

	void curar(){
		estaherido = false;
		tocando_suelo = true;
		animacion.SetBool ("Damage", false);
		AudioSource.PlayClipAtPoint (sonidoCurado, transform.position);
	}
}
