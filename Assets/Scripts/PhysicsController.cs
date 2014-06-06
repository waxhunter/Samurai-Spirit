using UnityEngine;
using System.Collections;

public class PhysicsController : MonoBehaviour {

	public float jumpForce;
	public float moveSpeed;

	public int direction = 1;

	public void ApplyJumpPhysics()
	{
		this.rigidbody2D.AddForce(new Vector2(0, jumpForce));
	}
	
	public void setVelocity(float speed)
	{
		rigidbody2D.velocity = new Vector2((float) ( direction * speed), rigidbody2D.velocity.y);
	}
}
