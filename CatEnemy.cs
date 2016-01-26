using UnityEngine;
using System.Collections;

public class CatEnemy : MonoBehaviour {

	public float speed = 5f;
	public float direction = -0.5f;
	public bool enemyColl = true;
	public int paramCollAnim = 0;


	// Use this for initialization
	void Start () {
		}

	// Update is called once per frame
	void Update () {

		GetComponent<Rigidbody2D> ().velocity = new Vector2 ( speed * direction, GetComponent<Rigidbody2D> ().velocity.y);
		transform.localScale = new Vector3 (direction, 0.3f, 1);

		}


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Box" || col.gameObject.tag == "Barrel" || col.gameObject.tag == "Car" ||col.gameObject.name == "Hero") {
			direction *= -1f;
		}
	}

}
