using UnityEngine;

public class Block : MonoBehaviour
{
	public int Value;

	/// <summary>
	/// Called when the ball hits the block
	/// </summary>
	public void Destroy(Ball from)
	{
		gameObject.SetActive(false); // hide block
		GameManager.Instance.Score += Value;
	}
}
