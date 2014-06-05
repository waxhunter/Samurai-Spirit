using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPhysicsController : MonoBehaviour {

	public PlayerActionController actionCtrl;
	public PlayerInputController inputCtrl;

	public float jumpForce;
	public float moveSpeed;
	public float fullSpeedAccelTime;

	public bool isOnGround = false;
	public float groundLineCastCorrector;

	public int direction = 1;

	public void ApplyJumpPhysics()
	{
		this.rigidbody2D.AddForce(new Vector2(0, jumpForce));
	}

	void setVelocity(float speed)
	{
		rigidbody2D.velocity = new Vector2((float) ( direction * speed), rigidbody2D.velocity.y);
	}

	void Update()
	{
		Vector2 collPos = this.collider2D.transform.position;

		List<float> test_pos_x = new List<float>() {0, this.collider2D.transform.localScale.x/2, -this.collider2D.transform.localScale.x/2};

		isOnGround = false;
		for(int i=0; i<3; i++)
		{
			RaycastHit2D[] raycast = Physics2D.LinecastAll(collPos, collPos + new Vector2(test_pos_x[i], - (this.collider2D.transform.localScale.y / 2 + groundLineCastCorrector)));

			foreach(RaycastHit2D rc in raycast)
			{
				if(rc.collider != null)
				{
					if(rc.collider.gameObject.name != "Player" && !rc.collider.isTrigger && !Physics2D.GetIgnoreLayerCollision(rc.collider.gameObject.layer, this.gameObject.layer))
					{
						isOnGround = true;
					}
				}

			}
		}

		actionCtrl.SetGround(isOnGround);

		if(actionCtrl.IsRunning())
			setVelocity(moveSpeed);

		else if(actionCtrl.IsRunningSlash() )
			setVelocity(moveSpeed * 2f);

		else if(actionCtrl.IsDashAttacking())
			setVelocity(moveSpeed * 2f);

		else if(actionCtrl.IsJumping() && inputCtrl.MovingHorizontal() )
			setVelocity(moveSpeed);

		else
			setVelocity(0f);

		transform.localScale = new Vector3(direction, 1, 1);
	}

}
