using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerActionController : MonoBehaviour {

	public Animator animCtrl;

	public PlayerInputController inputCtrl;
	public PlayerPhysicsController physicsCtrl;

	public int attackChargeDelayInMs;
	int attackChargePressTime = 0;

	public string idleAnim;
	public string blockingAnim;
	public List<string> attackingAnims;
	public List<string> runningAnims;
	public List<string> takeHitAnims;
	public string deathAnim;
	public string fallAnim;
	public string runningSlashAnim;
	public string dashAttackAnim;
	public string jumpingAnim;

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

	public bool IsRunningSlash()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName (runningSlashAnim))
		{
			return true;
		}
		else return false;
	}

	public bool IsDashAttacking()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName (dashAttackAnim))
		{
			return true;
		}
		else return false;
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

	public void TakeHit()
	{
		if(!IsBlocking() && !IsTakingHit())
		{
			animCtrl.SetBool ("Hit Taken 1", true);
		}
	}

	public void PerformJump()
	{
		if(animCtrl.GetBool ("OnFloor") == true && !IsAttacking () && !IsBlocking())
		{
			physicsCtrl.ApplyJumpPhysics();
		}
		
		animCtrl.SetTrigger("Jump");
	}

	public void PerformBlock()
	{
		animCtrl.SetBool ("Blocking", true);
	}

	public void StopBlock()
	{
		if(animCtrl.GetBool ("Blocking") == true)
		{
			animCtrl.SetBool ("Blocking", false);
		}
	}

	public void PerformAttackCharge(int direction)
	{
		if(!IsRunning())
		{
			if(attackChargePressTime < attackChargeDelayInMs)
			{
				attackChargePressTime += (int) (1000f * Time.deltaTime);
			}
			else
			{
				animCtrl.SetBool ("ChargeAttack", true);
				
				if(inputCtrl.MovingHorizontal () && animCtrl.GetBool("ChargeAttack") == true && !IsAttacking() && !animCtrl.GetBool ("DashAttack"))
				{
					physicsCtrl.direction = direction;
					animCtrl.SetTrigger ("DashAttack");
				}
				else
				{
					animCtrl.SetBool ("DashAttack", false);
				}
			}
		}
	}

	public void PerformAttack()
	{
		if(animCtrl.GetBool ("ChargeAttack") == true)
		{
			animCtrl.SetBool ("ChargeAttack", false);
			attackChargePressTime = 0;
		}
		else
		{
			if(IsAttacking() || IsIdle () || IsRunning ())
			{
				animCtrl.SetBool("Combo", true);
			}
		}
	}

	public void PerformMovement(int direction)
	{
		if(!IsAttacking() && !IsTakingHit ())
		{
			animCtrl.SetBool("Running", true);
			physicsCtrl.direction = direction;
		}
	}

	public void StopMovement()
	{
		animCtrl.SetBool("Running", false);
	}

	public void SetGround(bool isOnGround)
	{
		animCtrl.SetBool("OnFloor", isOnGround);
	}
}
