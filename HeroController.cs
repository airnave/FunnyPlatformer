using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class HeroController : MonoBehaviour {

	public float speed = 10f;
	public float forceJump = 700f;
	public float move {private get; set;}
	public bool jump {private get; set;}
	public bool reset { private get; set;}

	bool lookAtRight = true;

	public Transform groundCheck;

	public float groundRadius = 0.2f;

	public LayerMask whatIsGround;

	public bool grounded;


	public int score;
	public float spawnX, spawnY, spawnZ;


	public Animator animate;

	public GameObject m_oTextObject;

	public CoinsCanvas coinsCanvas;
	public CoinsCanvas levelCanvas;
//	public CoinsCanvas healthCanvas;


	public int health;
	public bool lockDamage = true;
	public bool damageAnim = false;

	public int activeScene;
	public int saveScore;
	public int saveHealth;

	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {



		score = PlayerPrefs.GetInt("Highscore");
		health = PlayerPrefs.GetInt ("Health");

		switch (health) {
		case 1:			
			Destroy (GameObject.Find ("Health" + 5));
			Destroy (GameObject.Find ("Health" + 4));
			Destroy (GameObject.Find ("Health" + 3));
			Destroy (GameObject.Find ("Health" + 2));
			break;

		case 2:
			
			Destroy (GameObject.Find ("Health" + 5));
			Destroy (GameObject.Find ("Health" + 4));
			Destroy (GameObject.Find ("Health" + 3));
			break;

		case 3: 
			
			Destroy (GameObject.Find ("Health" + 5));
			Destroy (GameObject.Find ("Health" + 4));
			break;

		case 4: 

			Destroy (GameObject.Find ("Health" + 5));
			break;

		}
//		activeScene = PlayerPrefs.GetInt("ActiveScene");

		animate = GetComponent<Animator>();

		spawnX = transform.position.x;
		spawnY = transform.position.y;
		spawnZ = transform.position.z;

		coinsCanvas = new CoinsCanvas ();
		levelCanvas = new CoinsCanvas ();
//		healthCanvas = new CoinsCanvas ();


		coinsCanvas.CreateUI (-270.0f, 250.0f, 0, "coinsText");
		levelCanvas.CreateUI (0.0f, 250.0f, 0, "levelText");
//		healthCanvas.CreateUI (-10.0f, 200.0f, 0, "healthText");


		spriteRenderer = GetComponent<SpriteRenderer>();

		activeScene = SceneManager.GetActiveScene ().buildIndex;

	}

	void FixedUpdate() {
//		move = Input.GetAxis ("Horizontal");

		var move = Input.GetAxis ("Horizontal");
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

	}


	void Update () {
		
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * speed, GetComponent<Rigidbody2D> ().velocity.y);

	

		if (lookAtRight && move < 0)
			FlipHorizontal ();
		else if (!lookAtRight && move > 0)
			FlipHorizontal ();
		
		if (damageAnim) 
			DamageFade (4.0f);

		if ((jump || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)) && grounded) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, forceJump));
			jump = false;
		}

		if (reset || Input.GetKeyDown (KeyCode.Backspace)) {
//			transform.position = new Vector3 (spawnX, spawnY, spawnZ);
//			PlayerPrefs.SetInt ("SpawnX", spawnX);
//			PlayerPrefs.SetInt ("SpawnY", spawnY);
//			PlayerPrefs.SetInt ("SpawnZ", spawnZ);
			PlayerPrefs.SetInt ("ActiveScene", SceneManager.GetActiveScene ().buildIndex);
			Debug.Log ("Active scene" + SceneManager.GetActiveScene ().buildIndex);
			PlayerPrefs.Save ();
			SceneManager.LoadScene ("Pause");
//			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			reset = false;
		}
		

		if (!grounded)
			animate.SetInteger ("moveAnimation", 2);
		else if (move != 0)
			animate.SetInteger ("moveAnimation", 1);
		else 
			animate.SetInteger ("moveAnimation", 0);

		  
		Text textCoinsCanvas = coinsCanvas.m_oTextObject.GetComponent<Text> ();
		textCoinsCanvas.alignment = TextAnchor.MiddleLeft;
		textCoinsCanvas.text = " x " + score;

		Text textLevelCanvas = levelCanvas.m_oTextObject.GetComponent<Text> ();
		textLevelCanvas.alignment = TextAnchor.MiddleCenter;
		textLevelCanvas.text = "Level : " + activeScene;

//		Text textHealthCanvas = healthCanvas.m_oTextObject.GetComponent<Text> ();
//		textHealthCanvas.text = "Health : " + health;
//		healthCanvas.m_oTextObject.GetComponent<RectTransform> ().localPosition = new Vector3 (360.0f, 260.0f, 0);


		if (health <= 0) {
			//transform.position = new Vector3 (spawnX, spawnY, transform.position.z);
//				PlayerPrefs.SetInt ("Highscore", 0);
//				PlayerPrefs.SetInt ("Health", 5);
				PlayerPrefs.SetInt ("ActiveScene", SceneManager.GetActiveScene ().buildIndex);
		    	Debug.Log ("Active scene" + SceneManager.GetActiveScene ().buildIndex);
				PlayerPrefs.Save ();

	//			SceneManager.LoadScene (SceneManager.GetActiveScene().name);

			SceneManager.LoadScene ("LoseMenu");
//			health = 5;
//			score = 0;
		}			




	}


	void FlipHorizontal() {
		lookAtRight = !lookAtRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D col) {


/*		if (col.gameObject.name == "DiePlatform") {
			transform.position = new Vector3 (spawnX, spawnY, transform.position.z);
			LosingHealth ();
		}
*/
		if (col.gameObject.tag == "Enemy" && lockDamage )
			StartCoroutine(DecreasingHealth());




	}

	IEnumerator DecreasingHealth() {
		lockDamage = false;
		LosingHealth ();
		damageAnim = true;
//		Debug.Log ("DecreasingHealth stop");
		yield return new WaitForSeconds(1);
//		Debug.Log ("DecreasingHealth working");
		lockDamage = true;
		damageAnim = false;
			
	}

	void DamageFade (float time) {
		Debug.Log ("Damagefade!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		Color tempcolor = spriteRenderer.color;
		tempcolor.a = Mathf.PingPong (Time.time * time, 1.0f);
		spriteRenderer.color = tempcolor;
	}

	void LosingHealth() {
		health--;
		Destroy (GameObject.Find ("Health" + (health+1)));
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name == "DiePlatform") {
			transform.position = new Vector3 (spawnX, spawnY, transform.position.z);
			LosingHealth ();
		}

		if (other.gameObject.name == "LevelEnd") {
			saveScore = score;
			PlayerPrefs.SetInt ("Highscore", saveScore);
			saveHealth = health;
			PlayerPrefs.SetInt ("Health", saveHealth);
			PlayerPrefs.Save ();
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}

		if (other.gameObject.tag == "Fire" && lockDamage)
			StartCoroutine (DecreasingHealth ());

		if (other.gameObject.tag == "Coin") {
			Destroy (other.gameObject);
			score++;
			Debug.Log ("+1 Coin");
		}

		if (other.gameObject.name == "EndMain") {
			//				PlayerPrefs.SetInt ("Highscore", 0);
			//				PlayerPrefs.SetInt ("Health", 5);
			//				PlayerPrefs.Save ();
			SceneManager.LoadScene ("MainMenu");
		}
	}
}




//	void OnGUI() {


//		GUI.Label(new Rect(10, 10, 100, 20), "Coins : " + score);

		//GUI.Box (new Rect(50, 50, 100, 20), "Coins : " + score);


//	}






