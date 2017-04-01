using UnityEngine;
using System.Collections;

/// <summary>
/// Stars movement controller.
/// </summary>
public class StarsMovement : MonoBehaviour
{

	public float movementSpeed;

	void Update ()
	{

		///There are 2 tileable Stars sprites in the scene, moving slowly one behind the other. 
		/// This snippet checks when the current sprite is out of the camera, then sends it behind the other sprite, which is by now already inside the camera.
		float finalSpeed = movementSpeed * Time.deltaTime;
		transform.position += Vector3.down * finalSpeed;

		float outOfCamPos = Camera.main.WorldToScreenPoint (transform.position).y;
		if (outOfCamPos < 0)
			transform.position += new Vector3 (transform.position.x, 28.68f);
	}
}
