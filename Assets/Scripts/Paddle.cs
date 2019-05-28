using UnityEngine;

public class Paddle : MonoBehaviour
{
	private GameManager manager => GameManager.Instance;

	private void Update()
	{
		FollowMouse();
	}

	/// <summary>
	/// Make our position the same as the mouse X position in world coordinates
	/// </summary>
	private void FollowMouse()
	{
		// get mouse pos in world space
		Vector2 mousePos = Input.mousePosition;
		Vector2 worldMousePos = manager.MainCamera.ScreenToWorldPoint(mousePos);

		// set our x position to mouse x
		Vector3 position = transform.position;
		position.x = worldMousePos.x;
		transform.position = position;
	}
}
