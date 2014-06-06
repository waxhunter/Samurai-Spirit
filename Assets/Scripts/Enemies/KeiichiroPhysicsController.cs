using UnityEngine;
using System.Collections;

public class KeiichiroPhysicsController : PhysicsController {

	public KeiichiroActionController actionCtrl;
	
	void Update () 
	{
		if(actionCtrl.IsRunning())
			setVelocity(moveSpeed);
		else
			setVelocity(0f);

		transform.localScale = new Vector3( (float) -direction, transform.localScale.y, 0);
	}
}
