 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeiichiroBehaviourController : MonoBehaviour {

	public KeiichiroActionController actionCtrl;
	public KeiichiroPhysicsController physicsCtrl;
	
	public float chasingDistance;
	public float attackRange;

	public bool chasePlayer;
	public bool attackPlayer;

	public GameObject player;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player Hit Area")
		{
			HitArea area = coll.gameObject.GetComponent<HitArea>();
			actionCtrl.TakeHit(coll.gameObject, area.damage, area.stagger, area.knockback);
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
	}

	void Update () {

		float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);

		if(distanceToPlayer < chasingDistance && distanceToPlayer > attackRange && !actionCtrl.IsAttacking())
		{
			if(chasePlayer) actionCtrl.PerformRun();
		}
		else
		{
			actionCtrl.StopRun ();

			if(distanceToPlayer <= attackRange && !actionCtrl.IsAttacking())
			{
				if(attackPlayer) actionCtrl.PerformAttack();
			}
		}



		if(Mathf.Abs(player.transform.position.x - this.transform.position.x) > physicsCtrl.spriteAdjustValue)
		{
			if(player.transform.position.x > this.transform.position.x && physicsCtrl.direction == -1)
			{
				actionCtrl.FaceDirection(1);
			}
			else if(player.transform.position.x < this.transform.position.x && physicsCtrl.direction == 1)
			{
				actionCtrl.FaceDirection(-1);
			}
		}
	}
}
