using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log ("Level load requested for: " + name);
		//Brick.breakableCount = 0;
		SceneManager.LoadScene(name);
	}

	public void QuitRequest () {
		Debug.Log ("I wanna Quit!");
		Application.Quit();
	}

	public void LoadNextLevel () {
		//Brick.breakableCount = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
