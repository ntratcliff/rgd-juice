using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[Header("Field")]
	public float FieldWidth;
	public Vector2 BlockSize;
	public int BlockColumns;
	public int BlockRows;
	public Vector2 BlockGridPadding;

	[Header("Prefabs")]
	public Block BlockPrefab;

	[Header("Scene References")]
	public Camera MainCamera;
	public Paddle Paddle;

	[HideInInspector]
	public List<Block> Blocks;

	[HideInInspector]
	public int Score;

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

		BuildField();
	}

	private void BuildField()
	{
		// make blocks
		Blocks = new List<Block>();
		Vector2 top = MainCamera.ViewportToWorldPoint(new Vector2(0.5f, 1f));
		Vector2 blockOffset = BlockSize + BlockGridPadding;
		Vector2 gridSize = new Vector2(BlockColumns - 1, BlockRows - 1) * blockOffset;
		Vector2 startPos = top - new Vector2(gridSize.x / 2f, BlockSize.y);
		for (int c = 0; c < BlockColumns; c++)
		{
			for (int r = 0; r < BlockRows; r++)
			{
				Blocks.Add(
					MakeBlock(
						position: startPos + new Vector2(c, -r) * blockOffset,
						size: BlockSize
					)
				);
			}
		}
	}

	private T MakeObject<T>(T prefab, Vector2 position, Vector3 size)
		where T : Component
	{
		// instantiate prefab
		T obj = Instantiate(prefab);

		// set position
		obj.transform.position = position;

		// set size of sprite and collider 
		// (this is a bit dirty but it's an example, not production code :^) )
		Transform sprite = obj.transform.Find("Sprite");
		Transform collider = obj.transform.Find("Collider");
		sprite.transform.localScale = collider.transform.localScale = size;

		return obj;
	}

	private Block MakeBlock(Vector2 position, Vector3 size) =>
		MakeObject(BlockPrefab, position, size);

}
