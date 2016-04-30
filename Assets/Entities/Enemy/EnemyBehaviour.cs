using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	public float health = 150f;
	public GameObject projectile;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private ScoreKeeper scoreKeeper;

	void Start () {
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}
	void Update () {
		float probability = shotsPerSecond * Time.deltaTime;
		if (Random.value < probability) {
			Fire ();
		}

	}

	void Fire () {
		Vector3 startPosition = transform.position + new Vector3 (0, -0.5f, 0);
		GameObject missile = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, -projectileSpeed);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			health -= missile.GetDamage ();
			missile.Hit();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die() {
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		scoreKeeper.Score (scoreValue);
		Destroy (gameObject);
	}
}





// THIS SCRIPT IS ATTACHED TO THE ENEMY PREFAB! 
// gameObject IS THE ENEMY! SO IT DESTROYS THE EMENY gameObject!