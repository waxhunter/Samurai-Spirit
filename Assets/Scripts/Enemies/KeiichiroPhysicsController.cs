using UnityEngine;
using System.Collections;

public class KeiichiroPhysicsController : PhysicsController {

	public KeiichiroActionController actionCtrl;

	public GameObject sprite;
	public float spriteAdjustValue;

	public void SetDirection(int direction)
	{
		this.direction = direction;
		transform.Translate(new Vector3(direction * spriteAdjustValue, 0f, 0f));
	}

	public void DeactivateHitAreas()
	{
		GameObject[] hitAreas = GameObject.FindGameObjectsWithTag("Enemy Hit Area");
		foreach(GameObject area in hitAreas)
		{
			if(area.transform.IsChildOf(this.transform))
				area.collider2D.enabled = false;
		}
	}
	
	void Update () 
	{
		if(!actionCtrl.IsAttacking())
		{
			DeactivateHitAreas();
		}

		if(actionCtrl.IsRunning())
			setVelocity(moveSpeed);
		else
			setVelocity(0f);

		transform.localScale = new Vector3( (float) -direction, transform.localScale.y, 0);
	}
}
