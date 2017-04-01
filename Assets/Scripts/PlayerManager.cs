using UnityEngine;
using System.Collections;


/// <summary>
/// Player manager. Manages Player ship. Player launching, reloading, getting killer, setting player's stats and upgrades are handled here.
/// </summary>
public class PlayerManager : MonoBehaviour
{

	GameObject player_obj;
	public GameObject playerShip;
	public PlayerScript playerHandle;
	public PlayerClass myPlayer01 = new PlayerClass ();
	public GameManager gManager;

	/// <summary>
	/// At the start of the gameplay, the player is launched with Player01 prefab instance. Player stats are set.
	/// </summary>
	void Awake ()
	{
		player_obj = Resources.Load ("Player01") as GameObject;
		LaunchPlayer ();
		SetPlayerStats (1, 1, 3, 3.5f, 300f);
	}

	/// <summary>
	/// Launches the player instance in the game and gets a handle on PlayerScript script.
	/// </summary>
	void LaunchPlayer ()
	{
		playerShip = GameObject.Instantiate (player_obj);
		playerHandle = playerShip.GetComponent<PlayerScript> ();
	}

	/// <summary>
	/// Sets the player stats. Player's level, health or number of hits it can take before dying , number of lives it can have, its speed and it's missile or weapon speed are set here.
	/// </summary>

	public void SetPlayerStats (int lvl, int health, int lives, float playerSpeed, float weaponSpeed)
	{
		myPlayer01.SetLevel (lvl);
		myPlayer01.SetPlayerHealth (health);
		myPlayer01.SetPlayerLives (lives);
		myPlayer01.SetPlayerSpeed (playerSpeed);
		myPlayer01.SetWeaponSpeed (weaponSpeed);
	}

	/// <summary>
	/// Kill sequence of the player is called. Upgrade counter is reset to 0 and ReloadPlayer coroutine is called.
	/// </summary>
	public void PlayerKilled ()
	{
		gManager.ResetUpgradeCounter ();
		StartCoroutine (ReloadPlayer ());
	}

	/// <summary>
	/// Decreases Player's life by 1. If the life is greater than 0, regenerates it after 2 blinks, or else calls for Game Over.
	/// </summary>
	IEnumerator ReloadPlayer ()
	{
		int life = myPlayer01.GetPlayerLives ();
		myPlayer01.SetPlayerLives (life - 1);
		myPlayer01.SetLevel (1);
		gManager.DisplayLives (myPlayer01.GetPlayerLives ());

		if (myPlayer01.GetPlayerLives () > 0) {
			LaunchPlayer ();
			playerShip.SetActive (false);
			yield return new WaitForSeconds (0.1f);
			playerShip.SetActive (true);
			yield return new WaitForSeconds (0.1f);
			playerShip.SetActive (false);
			yield return new WaitForSeconds (0.1f);
			playerShip.SetActive (true);
		} else {
			gManager.CallGameOver ();
		}
	}

	/// <summary>
	/// In current scenario we have only 3 levels. This function does a basic check whether Player level is between 0 and 3 and then calls UpgradePlayer from PlayerScript.
	/// </summary>
	public void PlayerUpgrade ()
	{
		int lvl = myPlayer01.GetLevel ();
		lvl = lvl + 1;
		if (lvl > 0 && lvl <= 3) {
			myPlayer01.SetLevel (lvl);
			playerHandle.UpgradePlayer ();
		}
	}

	void Update ()
	{
	
	}
}
