using UnityEngine;
using System.Collections;


/// <summary>
/// Individual enemy ships are controlled with this script.
/// </summary>
public class EnemyScript : MonoBehaviour
{

	EnemyClass currentEnemy = new EnemyClass ();
	GameObject projectile;
	bool bAttack = false;
	public EnemyManager eManager;
	public EnemyMissileMovement missileMovement;

	void Start ()
	{
		projectile = Resources.Load ("Projectile_Enemy") as GameObject;
		eManager = GameObject.Find ("EnemyManager").GetComponent<EnemyManager> ();
		bAttack = false;
	}

	/// <summary>
	/// Sets the enemy stats. hlth sets the health of enemy, as in how many hits will it take to destroy it.
	/// prjctl defines the number of missile projectiles the enemy will launch and prjctlSpeed defines its speed.
	/// </summary>

	public void SetEnemyStats (int hlth, int prjctl, int prjctlSpeed)
	{
		currentEnemy.health = hlth;
		currentEnemy.projectiles = prjctl;
		currentEnemy.missileSpeed = prjctlSpeed;
	}

	/// <summary>
	/// Initiates the attack as soon as the enemy is generated after a random time gap of 0.2 to 0.9 seconds after which Coroutine Attack is called.
	/// </summary>

	IEnumerator StartAttack ()
	{
		bAttack = true;
		float randTime = Random.Range (0.2f, 0.9f);
		yield return new WaitForFixedUpdate ();
		yield return new WaitForSeconds (randTime);
		StartCoroutine (Attack (currentEnemy.projectiles));
	}

	/// <summary>
	/// This Coroutine calls LoadMissile function in as many loops as defined by "rate", which basically is the number of projectiles the missile to be launched.
	/// While doing so, a gap of 2 seconds is given between every launch.
	/// </summary>

	IEnumerator Attack (int rate)
	{
		for (int i=0; i<rate; i++) {
			if (transform.childCount > 0) {
				LoadMissile ();
			}
			yield return new WaitForSeconds (2f);
			yield return new WaitForFixedUpdate ();
		}
	}

	/// <summary>
	/// Generates or "Loads" the missile, setting its speed and target with missileMovement.SetTarget();
	/// </summary>
	void LoadMissile ()
	{
		if (eManager.gManager.manager_playerHandle.playerHandle != null) {
			currentEnemy.targetPosition = eManager.gManager.manager_playerHandle.playerHandle.transform.position;
			currentEnemy.missile = GameObject.Instantiate (projectile);
			missileMovement = currentEnemy.missile.GetComponent<EnemyMissileMovement> ();
			currentEnemy.missile.transform.position = transform.GetChild (0).GetComponentInChildren<Transform> ().position;
			if (currentEnemy.targetPosition.x < currentEnemy.missile.transform.position.x) {
				currentEnemy.targetPosition.x -= 5;
			} else if (currentEnemy.targetPosition.x > currentEnemy.missile.transform.position.x) {
				currentEnemy.targetPosition.x += 5;
			}
			
			if (currentEnemy.targetPosition.y < currentEnemy.missile.transform.position.y) {
				currentEnemy.targetPosition.y -= 5;

			} else if (currentEnemy.targetPosition.y > currentEnemy.missile.transform.position.y) {
				currentEnemy.targetPosition.y += 5;

			}
			missileMovement.SetTarget (currentEnemy.missileSpeed, currentEnemy.targetPosition);
		}
	}


	/// <summary>
	/// Called by Player's missile projectile script when it hits the enemy ship. This decreases the enemy's health and calls Killenemy() if health turns to 0;
	/// </summary>
	public void TakeHit ()
	{
		currentEnemy.health = currentEnemy.health - 1;
		if (currentEnemy.health <= 0) {
			KillEnemy ();
		}
	}

	/// <summary>
	/// Kills the enemy and sets Player's score. If you kill the boss, which is defined by the number of projectiles being more than 2 in the current scenario, you get 50 points per boss.
	/// If you kill the other 2 enemies, you get 10 points per ship. UpgradeCounter is called to check if the player is to be upgraded.
	/// </summary>
	void KillEnemy ()
	{
		eManager.gManager.UpgradeCounter ();
		if (currentEnemy.projectiles > 2) {
			eManager.gManager.SetScore (50);
		} else {
			eManager.gManager.SetScore (10);
		}
		StopCoroutine ("Attack");
		Destroy (this.gameObject);
	}
	
	void Update ()
	{
		///Calls StartAttack sequence.
		if (!bAttack) {
			StartCoroutine (StartAttack ());
		}

		///Checks if the enemy ship is out of the screen, and then destroys it from the scene.
		if (transform.childCount > 0) {
			Vector3 craftPos = transform.GetChild (0).GetComponentInChildren<Transform> ().position;
			if (Camera.main.WorldToScreenPoint (craftPos).y < -3 || Camera.main.WorldToScreenPoint (craftPos).y > Screen.height + 3) {
				StopCoroutine ("Attack");
				Destroy (this.gameObject);
			}
			if (Camera.main.WorldToScreenPoint (craftPos).y > Screen.width + 3) {
				StopCoroutine ("Attack");
				Destroy (this.gameObject);
			}
		}
	}
}
