using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy missile movement. Individual enemy missiles are controlled from here.
/// </summary>
public class EnemyMissileMovement : MonoBehaviour
{

	float speed;
	Vector3 targetPosition;

	void Start ()
	{
	
	}

	/// <summary>
	/// The missile projectile destructs on hitting Player and Player's TakeHit function is called which decreases player health.
	/// </summary>

	void OnTriggerEnter (Collider col)
	{
		if (col.name.Contains ("Player")) {
			PlayerScript player01 = col.GetComponent<PlayerScript> ();
			player01.TakeHit ();
			Destroy (this.gameObject);
		}
	}

	/// <summary>
	/// This is called from EnemyScript's LoadMissile function and sets the missile projectile's speed and position of its target, which being the Player
	/// </summary>

	public void SetTarget (float spd, Vector3 targetpos)
	{
		speed = spd;
		targetPosition = targetpos;
	}

	void Update ()
	{
		///Moves the missile to its target at calculated speed.
		float finalSpeed = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPosition, finalSpeed);

		///If the missile goes out of the screen, it destroys itself.
		if (Camera.main.WorldToScreenPoint (transform.position).y < 0) {
			Destroy (this.gameObject);
		}
	}
}
