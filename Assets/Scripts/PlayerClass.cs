using UnityEngine;
using System.Collections;

/// <summary>
/// Player class. It has some basic parameters regarding player ship.
/// </summary>
public class PlayerClass
{
	private int health;
	private int lives;
	private int level;
	public GameObject weapon;
	private float playerSpeed;
	private float weaponSpeed;
	
	public int GetPlayerHealth ()
	{
		return health;
	}
	public void SetPlayerHealth (int hlth)
	{
		health = hlth;
	}
	
	public int GetPlayerLives ()
	{
		return lives;
	}
	
	public void SetPlayerLives (int plivs)
	{
		lives = plivs;
	}

	public int GetLevel ()
	{
		return level;
	}
	public void SetLevel (int lvl)
	{
		level = lvl;
	}

	public float GetPlayerSpeed ()
	{
		return playerSpeed;
	}
	public void SetPlayerSpeed (float spd)
	{
		playerSpeed = spd;
	}
	public float GetWeaponSpeed ()
	{
		return weaponSpeed;
	}
	public void SetWeaponSpeed (float spd)
	{
		weaponSpeed = spd;
	}
}
