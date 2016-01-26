using UnityEngine;
using System.Collections;

public class DogEnemy : MonoBehaviour {

	public float speed = 5f;
	public float direction = -0.7f;
	public bool enemyColl = true;
	public int paramCollAnim = 0;
	public Animator animateEnemy;


	// Use this for initialization
	void Start () {
		
		animateEnemy = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		
		GetComponent<Rigidbody2D> ().velocity = new Vector2 ( speed * direction, GetComponent<Rigidbody2D> ().velocity.y);
		transform.localScale = new Vector3 (direction, 0.7f, 1);


		if (enemyColl)
			animateEnemy.SetInteger ("moveAnimation", paramCollAnim);
		else {
			animateEnemy.SetInteger ("moveAnimation", paramCollAnim);
			enemyColl = true;
			StartCoroutine(AnimateCol());
			}
		
	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Box" || col.gameObject.tag == "Barrel" || col.gameObject.tag == "Car" ||col.gameObject.name == "Hero" ) {
			direction *= -1f;
			paramCollAnim = 1;
			enemyColl = false;
		}
	}

	IEnumerator AnimateCol() {
		
		yield return new WaitForSeconds (0.5f);
		paramCollAnim = 0;
	}
}
