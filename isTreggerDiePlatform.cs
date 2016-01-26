using UnityEngine;
using System.Collections;

public class isTreggerDiePlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Box" || other.gameObject.tag == "Enemy")
			Destroy (other.gameObject);
	}

}

