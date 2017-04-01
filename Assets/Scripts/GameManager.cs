using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Game manager class. This has basic controls over Player, Enemy and the UI within the Gameplay scene.
/// </summary>
public class GameManager : MonoBehaviour
{

	public EnemyManager manager_enemyHandle;
	public PlayerManager manager_playerHandle;
	GameObject player01;
	public static bool bPause;
	private int score;
	List<int> bossWorthyScores = new List<int>{200,400,666,900,1024,2048,4096,8192,11000,13000};
	public Text txt_Score;
	public Text txt_Lives;
	int upgradeCount;

	void Start ()
	{
		upgradeCount = 0;
		bPause = false;
		Time.timeScale = 1.0f;
		SetScore (0);
		DisplayLives (manager_playerHandle.myPlayer01.GetPlayerLives ());
	}

	/// <summary>
	/// Pause functionality.
	/// </summary>
	public void Pause ()
	{
		if (!bPause) {
			bPause = true;
			Time.timeScale = 0;
		} else {
			bPause = false;
			Time.timeScale = 1.0f;
		}
	}

	/// <summary>
	/// Keeps setting the Player score and updating it on the UI.
	/// </summary>

	public void SetScore (int scr)
	{
		score += scr;
		txt_Score.text = "Score: " + score.ToString ();
		if (score == 50) {
			manager_enemyHandle.StopUnleashing ();
			StartCoroutine (manager_enemyHandle.UnleashEnemies (2, 5f));
		} else if (score == 100) {

			manager_enemyHandle.StopUnleashing ();
			StartCoroutine (manager_enemyHandle.UnleashEnemies (2, 4f));
		} else if (bossWorthyScores.Contains (score)) {
			manager_enemyHandle.StopUnleashing ();
			StartCoroutine (manager_enemyHandle.UnleashEnemies (2, 8f));
			manager_enemyHandle.UnleashBoss ();
		} else if (score > 200 && !bossWorthyScores.Contains (score)) {
			manager_enemyHandle.StopUnleashing ();
			StartCoroutine (manager_enemyHandle.UnleashEnemies (2, 5f));
		}

	}

	/// <summary>
	/// Returns the score to any other class that would require it.
	/// </summary>
	public int GetScore ()
	{
		return score;
	}


	/// <summary>
	/// Displays the lives. It is called whenever you start the game or your life is lost in current scenario. Can also be used for 1ups if game is to be enhanced.
	/// </summary>
	public void DisplayLives (int life)
	{
		txt_Lives.text = "Lives: " + life.ToString ();
	}

	/// <summary>
	/// This is called whenever you kill an enemy. If you reach 13 kills or 26 kills, you are upgraded to one level above.
	/// </summary>
	public void UpgradeCounter ()
	{
		upgradeCount++;
		if (upgradeCount == 13) {
			manager_playerHandle.PlayerUpgrade ();
		} else if (upgradeCount == 26) {
			manager_playerHandle.PlayerUpgrade ();
		}
	}

	/// <summary>
	/// Resets the upgrade counter. Called when you die or game is started.
	/// </summary>
	public void ResetUpgradeCounter ()
	{
		upgradeCount = 0;
	}

	/// <summary>
	/// When you lose all your lives, CallGameOver is called to check and set high scores, if any and load the GameOver scene.
	/// </summary>
	public void CallGameOver ()
	{
		int highScore = PlayerPrefs.GetInt ("HighScore");
		if (GetScore () > highScore) {
			PlayerPrefs.SetInt ("HighScore", GetScore ());
		}
		GameOver.SetYourScore (GetScore ());
		Application.LoadLevel ("GameOver");
	}


	void Update ()
	{
	
	}
}
