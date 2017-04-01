using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used for simply cleaning up the explosion sprite that gets generated when a ship is destroyed.
/// </summary>
public class CleanExplosion : MonoBehaviour
{
	
	void Start ()
	{
		StartCoroutine (CleanUp ());
	}

	IEnumerator CleanUp ()
	{
		yield return new WaitForSeconds (0.06f);
		yield return new WaitForFixedUpdate ();
		Destroy (this.gameObject);
	}

	void Update ()
	{
	
	}
}
