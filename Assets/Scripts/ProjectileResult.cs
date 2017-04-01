using UnityEngine;
using System.Collections;

/// <summary>
/// Player missile projectile controller script.
/// </summary>
public class ProjectileResult : MonoBehaviour
{

	GameObject explodePrefab;

	void Start ()
	{
		explodePrefab = Resources.Load ("Explosion") as GameObject;
	}

	/// <summary>
	/// When the Player's missile hits an enemy ship, enemy ship's TakeHit() is called.
	/// </summary>

	void OnTriggerEnter (Collider col)
	{
		if (col.name.Contains ("EnemyCraft")) {
			GameObject explosion = GameObject.Instantiate (explodePrefab);
			explosion.transform.position = col.transform.position;
			EnemyScript enemyHandle = col.GetComponentInParent<EnemyScript> ();
			enemyHandle.TakeHit ();
			Destroy (this.gameObject);
		}
	}
	
	void Update ()
	{
		///Checks if the missile is out of the camera then destroys it off the scene.
		if (Camera.main.WorldToScreenPoint (transform.position).y > Screen.height) {
			Destroy (this.gameObject);
		}
	
	}
}
