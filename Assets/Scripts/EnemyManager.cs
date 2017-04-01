using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy manager. Manages generating enemy ships.
/// </summary>
public class EnemyManager : MonoBehaviour
{
	public GameObject enemy01;
	public GameObject enemy02;
	public GameObject enemy03;
	EnemyScript enemyHandle;
	GameObject currentEnemy;
	public GameManager gManager;

	void Awake ()
	{
		enemy01 = Resources.Load ("Enemy01") as GameObject;
		enemy02 = Resources.Load ("Enemy02") as GameObject;
		enemy03 = Resources.Load ("Enemy03") as GameObject;
	}

	void Start ()
	{
		StartCoroutine (UnleashEnemies (1, 2.5f));  
	}


	/// <summary>
	/// Simply keeps calling the Generate function at given timeGap.
	/// </summary>

	public IEnumerator UnleashEnemies (int strnt, float timeGap)
	{
		yield return new WaitForSeconds (timeGap);
		EnemyGenerate (strnt);
		yield return new WaitForFixedUpdate ();
		StartCoroutine (UnleashEnemies (strnt, timeGap));
	}

	/// <summary>
	/// Random enemy ships are generated according to the Strength parameter. It basically defines how many types of ships will be generated. 
	/// Enemy01 flies vertically in and out, while Enemy02 flies horizontally in and out
	/// </summary>

	void EnemyGenerate (int strength)
	{
		float randXPos, randYPos;
		Vector3 enemyStarPos;
		int randEnemy;
		switch (strength) {
		case 1:
			currentEnemy = GameObject.Instantiate (enemy01);
			enemyHandle = currentEnemy.GetComponent<EnemyScript> ();
			enemyHandle.SetEnemyStats (1, 1, 6);

			randXPos = Random.Range (6, Screen.width / 1.25f);
			enemyStarPos = new Vector3 (randXPos, Screen.height * 1.1f);
			currentEnemy.transform.position = Camera.main.ScreenToWorldPoint (enemyStarPos); 
			break;

		case 2:
			randEnemy = Random.Range (1, 11);
			if (randEnemy <= 4) {
				currentEnemy = GameObject.Instantiate (enemy02);
				enemyHandle = currentEnemy.GetComponent<EnemyScript> ();
				enemyHandle.SetEnemyStats (1, 2, 6);

				randYPos = Random.Range (Screen.height / 1.4f, Screen.height / 1.28f);
				float XPos = Camera.main.WorldToScreenPoint (currentEnemy.transform.position).x;
				enemyStarPos = new Vector3 (XPos, randYPos);
				currentEnemy.transform.position = Camera.main.ScreenToWorldPoint (enemyStarPos);
			} else {
				currentEnemy = GameObject.Instantiate (enemy01);
				enemyHandle = currentEnemy.GetComponent<EnemyScript> ();
				enemyHandle.SetEnemyStats (1, 1, 6);

				randXPos = Random.Range (6, Screen.width / 1.25f);
				enemyStarPos = new Vector3 (randXPos, Screen.height * 1.1f);
				currentEnemy.transform.position = Camera.main.ScreenToWorldPoint (enemyStarPos); 
			}
			break;

		default:
			currentEnemy = GameObject.Instantiate (enemy01);
			enemyHandle = currentEnemy.GetComponent<EnemyScript> ();
			enemyHandle.SetEnemyStats (1, 1, 6);
			break;
		}


	}

	/// <summary>
	/// A Boss ship is generated that has more health and more projectiles to shoot.
	/// </summary>
	public void UnleashBoss ()
	{
		currentEnemy = GameObject.Instantiate (enemy03);
		enemyHandle = currentEnemy.GetComponent<EnemyScript> ();
		enemyHandle.SetEnemyStats (6, 8, 6);
	}

	public void StopUnleashing ()
	{
		StopCoroutine ("UnleashEnemies");
	}
	
	void Update ()
	{
	
	}
}
