using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPhysicsController : PhysicsController {

	public PlayerActionController actionCtrl;
	public PlayerInputController inputCtrl;

	public float fullSpeedAccelTime;

	public bool isOnGround = false;
	public float groundLineCastCorrector;
	public float horizontalLineCastCorrector;

	public void DeactivateHitAreas()
	{
		GameObject[] hitAreas = GameObject.FindGameObjectsWithTag("Player Hit Area");
		foreach(GameObject area in hitAreas)
		{
			if(area.transform.IsChildOf(this.transform))
			{
				if(area.collider2D.enabled == true)
				{
					area.collider2D.enabled = false;
				}
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Enemy Hit Area")
		{
			actionCtrl.TakeHit();
		}
	}

	void FixedUpdate()
	{
	}

	void Update()
	{
		if(!actionCtrl.IsAttacking())
		{
			DeactivateHitAreas();
		}


		Vector2 collPos = this.transform.position;
		BoxCollider2D collider = this.GetComponent<BoxCollider2D>();

		List<float> test_pos_x = new List<float>() {0, (collider.size.x/2 - horizontalLineCastCorrector), (-collider.size.x/2 + horizontalLineCastCorrector)};

		isOnGround = false;
		for(int i=0; i<3; i++)
		{
			RaycastHit2D[] raycast = Physics2D.LinecastAll(collPos, collPos + new Vector2(test_pos_x[i], - (collider.size.y / 2 + groundLineCastCorrector)));

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
		{
			bool found = false;

			List<float> test_pos_y = new List<float>() {0, (collider.size.y/2 - groundLineCastCorrector), (-collider.size.y/2 + groundLineCastCorrector)};

			for(int i=0; i<3; i++)
			{
				RaycastHit2D[] raycast = Physics2D.LinecastAll (collPos, collPos + new Vector2(direction * (collider.size.x/2 + horizontalLineCastCorrector), test_pos_y[i]));

				foreach(RaycastHit2D rc in raycast)
				{
					if(rc.collider.gameObject.name != "Player" && !rc.collider.isTrigger && !Physics2D.GetIgnoreLayerCollision(rc.collider.gameObject.layer, this.gameObject.layer))
					{
						found = true;
					}
				}

			}

			if(found == false)
			{
				setVelocity(moveSpeed);
			}
		}
		else
			setVelocity(0f);

		transform.localScale = new Vector3(direction, 1, 1);
	}

	void LateUpdate()
	{

	}
}
