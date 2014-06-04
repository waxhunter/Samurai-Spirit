using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPhysics : MonoBehaviour {

	public bool isOnGround = false;
	public float groundLineCastCorrector;

	public Animator animCtrl;

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
					if(rc.collider.gameObject.name != "Player")
					{
						isOnGround = true;
						print (rc.collider.name);
					}
				}

			}
		}

		animCtrl.SetBool("OnFloor", isOnGround);
	}

}
