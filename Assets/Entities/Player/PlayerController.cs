using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public float padding = 1f;
	public GameObject projectile;
	public AudioClip fireSound;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250f;

	float xmin;
	float xmax;

	void Start () {
		// distance between plane and main camera (mainly for use in 3d games)
		float distance = transform.position.z - Camera.main.transform.position.z;
		// camera calculates the Boundarys of the screen - boundaries of playspace (left corner is 0, middle 0.5, right 1)
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		// sets the actual boundaries (note left- and right-most depends on screen(viewport) size)
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}

	void Fire () {
		Vector3 offset = new Vector3 (0, 1, 0);
		GameObject beam = Instantiate (projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, projectileSpeed,0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}

		// when you move the left and right keys, the transform position of player changes accordingly (note speed)
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		// Restrict the player to the gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}

	void OnTriggerEnter2D (Collider2D collider) {
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			Debug.Log ("Player collided with a missile");
			health -= missile.GetDamage ();
			missile.Hit();
			if (health <= 0) {
				Die ();
			}
		}
	}

	void Die() {
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		man.LoadLevel ("Win");
		Destroy (gameObject);
	}
}

