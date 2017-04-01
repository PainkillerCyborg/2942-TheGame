using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy class. It has some basic parameters and values regarding enemy ships. The Integer value "projectiles" indicates the number of missiles an enemy ship fires. 
/// </summary>
public class EnemyClass
{
	public int health;
	public int projectiles;
	public GameObject missile;
	public float missileSpeed;
	public Vector3 targetPosition;
}