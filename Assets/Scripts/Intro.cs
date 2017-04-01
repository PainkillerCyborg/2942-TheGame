using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Intro scene manager.
/// </summary>
public class Intro : MonoBehaviour
{

	int highScore;
	public Text txt_highScore;

	/// <summary>
	/// Displays highscore.
	/// </summary>
	void Start ()
	{
		highScore = PlayerPrefs.GetInt ("HighScore");
		txt_highScore.text = "High Score: " + highScore.ToString ();
	}

	/// <summary>
	/// Starts the game by calling Gameplay scene.
	/// </summary>
	public void StartGame ()
	{
		Application.LoadLevel ("Gameplay");
	}

	void Update ()
	{
	
	}
}
