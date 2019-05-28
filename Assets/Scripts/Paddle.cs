using UnityEngine;

public class Paddle : MonoBehaviour
{
	private GameManager manager => GameManager.Instance;

	private Vector2 size;

	private void Awake()
	{
		size = transform.Find("Sprite").localScale;
	}

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

		// set our x position to mouse x, clamped to field dimensions
		Vector3 position = transform.position;
		position.x = Mathf.Clamp(
			value: worldMousePos.x,
			min: (-manager.FieldWidth + size.x) / 2f,
			max: (manager.FieldWidth - size.x) / 2f
		);
		transform.position = position;
	}
}
