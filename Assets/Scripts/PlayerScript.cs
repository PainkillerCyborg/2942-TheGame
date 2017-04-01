using UnityEngine;
using System.Collections;

/// <summary>
/// Player's ship is handled here.
/// </summary>
public class PlayerScript : MonoBehaviour
{

	public PlayerManager pManager;
	GameObject projectile;
	GameObject explodePrefab;
	public Sprite spt_PlayerLevel01, spt_PlayerLevel02, spt_PlayerLevel03;

	/// <summary>
	/// Player missile projectile is fetched and Level 01 player sprite is loaded.
	/// </summary>
	void Awake ()
	{
		explodePrefab = Resources.Load ("Explosion") as GameObject;
		pManager = GameObject.Find ("PlayerManager").GetComponent<PlayerManager> ();
		projectile = Resources.Load ("Projectile_Level01") as GameObject;
		transform.gameObject.GetComponent<SpriteRenderer> ().sprite = spt_PlayerLevel01;
	}

	/// <summary>
	/// If the player hits an enemy ship, it is killed instantly.
	/// </summary>

	void OnTriggerEnter (Collider col)
	{
		if (col.name.Contains ("EnemyCraft")) {
			Destroy (col.gameObject);
			KillPlayer ();
		}
	}

	void Update ()
	{
		////Movements
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			if (Camera.main.WorldToScreenPoint (transform.position).y < Screen.height)
				transform.position += Vector3.up * pManager.myPlayer01.GetPlayerSpeed () * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			if (Camera.main.WorldToScreenPoint (transform.position).x > 0)
				transform.position += Vector3.left * pManager.myPlayer01.GetPlayerSpeed () * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			if (Camera.main.WorldToScreenPoint (transform.position).y > 0)
				transform.position += Vector3.down * pManager.myPlayer01.GetPlayerSpeed () * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			if (Camera.main.WorldToScreenPoint (transform.position).x < Screen.width)
				transform.position += Vector3.right * pManager.myPlayer01.GetPlayerSpeed () * Time.deltaTime;
		}


		///Firing
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.RightControl) && !GameManager.bPause) {
			pManager.myPlayer01.weapon = GameObject.Instantiate (projectile);
			pManager.myPlayer01.weapon.transform.position = transform.position + new Vector3 (-0.101f, 0.274f);
			Rigidbody weaponRigidBody = pManager.myPlayer01.weapon.transform.GetComponent<Rigidbody> ();
			weaponRigidBody.velocity = Vector3.up * pManager.myPlayer01.GetWeaponSpeed () * Time.deltaTime;
		}
	}

	/// <summary>
	/// When an enemy missile hits the player ship, its health is decreased and if health is 0, then the player is killed.
	/// </summary>
	public void TakeHit ()
	{
		pManager.myPlayer01.SetPlayerHealth (pManager.myPlayer01.GetPlayerHealth () - 1);
		if (pManager.myPlayer01.GetPlayerHealth () <= 0) {
			KillPlayer ();
		}
	}

	/// <summary>
	/// Kills the player.
	/// </summary>
	public void KillPlayer ()
	{
		GameObject explosion = GameObject.Instantiate (explodePrefab);
		explosion.transform.position = transform.position;
		pManager.PlayerKilled ();
		Destroy (this.gameObject);
	}

	/// <summary>
	/// Upgrades the player. Changes its sprites and projectile numbers visually on Level 02 and Level 03, since in this scenario we have only 3 levels for the player. On 2nd and 3rd level, player's health is increased too.
	/// </summary>
	public void UpgradePlayer ()
	{
		if (pManager.myPlayer01.GetLevel () == 2) {
			transform.gameObject.GetComponent<SpriteRenderer> ().sprite = spt_PlayerLevel02;
			projectile = Resources.Load ("Projectile_Level02") as GameObject;
			pManager.SetPlayerStats (pManager.myPlayer01.GetLevel (), 3, pManager.myPlayer01.GetPlayerLives (), pManager.myPlayer01.GetPlayerSpeed (), pManager.myPlayer01.GetWeaponSpeed ());
		} else if (pManager.myPlayer01.GetLevel () == 3) {
			transform.gameObject.GetComponent<SpriteRenderer> ().sprite = spt_PlayerLevel03;
			pManager.SetPlayerStats (pManager.myPlayer01.GetLevel (), 6, pManager.myPlayer01.GetPlayerLives (), pManager.myPlayer01.GetPlayerSpeed (), pManager.myPlayer01.GetWeaponSpeed ());
			projectile = Resources.Load ("Projectile_Level03") as GameObject;
		}
	}


}
