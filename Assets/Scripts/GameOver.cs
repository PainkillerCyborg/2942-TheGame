using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Game over manager script.
/// </summary>
public class GameOver : MonoBehaviour
{

	public Text txt_YourScore;
	public Text txt_HighScore;
	private static int yourScore;
	int highScore;

	/// <summary>
	/// High score and your current score is displayed.
	/// </summary>
	void Start ()
	{
		txt_YourScore.text = "Your Score: " + yourScore.ToString ();
		int highScore = PlayerPrefs.GetInt ("HighScore");
		txt_HighScore.text = "High Score: " + highScore.ToString ();
	}

	/// <summary>
	/// Sets your current score in a local variable.
	/// </summary>
	public static void SetYourScore (int scr)
	{
		yourScore = scr;
	}

	/// <summary>
	/// Restarts the game by calling Gameplay scene.
	/// </summary>
	public void Restart ()
	{
		Application.LoadLevel ("Gameplay");
	}
	

	void Update ()
	{
	
	}
}
