using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	public Camera MainCamera;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("[GameManager] More than one instance of " +
				"GameManager in the scene! Destroying self...");
			DestroyImmediate(this);
			return;
		}
	}
}
