using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	public float Size = 0.3f;
	public float Speed = 1f;

	public SpriteRenderer SpriteRenderer;
	public CircleCollider2D Collider;

	public Rigidbody2D Body { get; private set; }

	private Vector2 velocity;

	private void Awake()
	{
		// set dimensions
		SpriteRenderer.transform.localScale = Vector3.one * Size;
		Collider.radius = Size / 2f;

		// using a rigidbody just to get collision events becuase I'm lazy
		Body = GetComponent<Rigidbody2D>();

		velocity = -Vector2.one * Speed;
	}

	private void FixedUpdate()
	{
		transform.Translate(velocity * Time.fixedDeltaTime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// check if colliding with block
		Block block = collision.collider.GetComponentInParent<Block>();
		if (block)
		{
			block.Destroy(this);
		}

		// reflect velocity across surface normal
		velocity = Vector2.Reflect(
			inDirection: velocity,
			inNormal: collision.GetContact(0).normal
		);

		// surface normals can be a bit unreliable in unity, 
		// preserve 45 degree direction and speed after reflect
		velocity = new Vector2(
			x: Mathf.Sign(velocity.x),
			y: Mathf.Sign(velocity.y)
		) * Speed;
	}
}
