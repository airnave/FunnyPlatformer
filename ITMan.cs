using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ITMan : MonoBehaviour {

	public int activeScene;


	public void StartGame() {


		PlayerPrefs.SetInt ("Highscore", 0);
		PlayerPrefs.SetInt ("Health", 5);
		PlayerPrefs.SetInt("ActiveScene", 0);
		PlayerPrefs.Save ();
		SceneManager.LoadScene ("Scene1");

	}

	public void RestartLevel() {
		

		PlayerPrefs.SetInt ("Highscore", 0);
		PlayerPrefs.SetInt ("Health", 5);
		PlayerPrefs.Save ();
		activeScene = PlayerPrefs.GetInt("ActiveScene");
		SceneManager.LoadScene (activeScene);

	}

	public void Continue() {

//		PlayerPrefs.SetInt ("Health", 5);
//		PlayerPrefs.Save ();
		activeScene = PlayerPrefs.GetInt("ActiveScene");
		if (activeScene == 0)
			SceneManager.LoadScene ("Scene1");
		SceneManager.LoadScene (activeScene);

	}
}


