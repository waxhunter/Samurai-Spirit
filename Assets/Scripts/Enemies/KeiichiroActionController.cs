using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeiichiroActionController : ActionController {

	public KeiichiroPhysicsController physicsCtrl;
	public KeiichiroStatusController statusCtrl;
	public KeiichiroEffectsController effectsCtrl;

	public string idleAnim;
	public string blockingAnim;
	public List<string> attackingAnims;
	public List<string> runningAnims;
	public List<string> takeHitAnims;
	public string deathAnim;
	public string fallAnim;
	public string jumpingAnim;

	public GameObject lastHitArea;
	bool takingHit = false;

	public bool IsIdle()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName (idleAnim))
		{
			return true;
		}
		else return false;
	}
	
	public bool IsBlocking()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName (blockingAnim))
		{
			return true;
		}
		else return false;
	}
	
	public bool IsAttacking()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		foreach(string anim in attackingAnims)
		{
			if(asi.IsName (anim))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsJumping()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName (jumpingAnim))
		{
			return true;
		}
		else return false;
	}
	
	public bool IsTakingHit()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		foreach(string anim in takeHitAnims)
		{
			if(asi.IsName (anim))
			{
				return true;
			}
		}
		return false;
	}
	
	public bool IsRunning()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		foreach(string anim in runningAnims)
		{
			if(asi.IsName (anim))
			{
				return true;
			}
		}
		return false;
	}

	public void PerformRun()
	{
		animCtrl.SetBool ("Running", true);
	}

	public void StopRun()
	{
		animCtrl.SetBool ("Running", false);
	}

	public void PerformAttack()
	{
		animCtrl.SetBool ("Attack", true);
	}

	public void TakeHit(GameObject hitArea, float damage, bool stagger, bool knockback)
	{
		if(!IsBlocking() && !IsTakingHit() && lastHitArea == null)
		{
			lastHitArea = hitArea;

			if(knockback)
				animCtrl.SetBool ("Fall", true);
			else if(stagger)
				animCtrl.SetBool ("Hit Taken", true);

			takingHit = true;

			statusCtrl.TakeDamage(damage);
			physicsCtrl.DeactivateHitAreas();
			effectsCtrl.TakeHit();
		}
	}

	void Update()
	{
		if(lastHitArea != null)
		{
			if(lastHitArea.collider2D.enabled == false)
			{
				lastHitArea = null;
				StopTakingHit ();
			}
		}
	}

	public void StopTakingHit()
	{
		takingHit = false;
	}

	public void FaceDirection(int direction)
	{
		if(!IsAttacking() && !IsTakingHit ())
		{
			physicsCtrl.SetDirection(direction);
		}
	}

	public void Die()
	{
		animCtrl.SetBool("Death", true);
	}
}
