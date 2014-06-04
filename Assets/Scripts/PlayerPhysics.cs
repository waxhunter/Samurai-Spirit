using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

	public bool isOnGround = false;
	public float groundLineCastCorrector;

	public Animator animCtrl;

	void Update()
	{
		Vector2 collPos = this.collider2D.transform.position;
		RaycastHit2D[] raycast = Physics2D.LinecastAll(collPos, collPos + new Vector2(0, - (this.collider2D.transform.localScale.y / 2 + groundLineCastCorrector)));

		isOnGround = false;
		foreach(RaycastHit2D rc in raycast)
		{
			if(rc.collider != null)
			{
				if(rc.collider.gameObject.name == "Stage")
				{
					isOnGround = true;
					print (rc.collider.name);
				}
			}

		}

		animCtrl.SetBool("OnFloor", isOnGround);
	}

}
