using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage = 100f;

	public float GetDamage() {
		return damage;
	}

	public void Hit() {
		Destroy (gameObject);
	}
}






// THIS SCRIPT IS ATTACHED TO THE PLAYER LASER PREFAB!! SO WHEN IT HIT() THE ENEMY, IT DESTROYS ITSELF!
