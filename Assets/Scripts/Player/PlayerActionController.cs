using UnityEngine;
using System.Collections;

public class PlayerActionController : MonoBehaviour {

	public Animator animCtrl;

	public PlayerInputController inputCtrl;
	public PlayerPhysicsController physicsCtrl;

	public int attackChargeDelay;
	int attackChargePressTime = 0;

	public bool IsIdle()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName ("Hibiki - Idle"))
		{
			return true;
		}
		else return false;
	}

	public bool IsAttacking()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);

		if(asi.IsName ("Hibiki - Attack1") || asi.IsName ("Hibiki - Attack 2") || asi.IsName ("Hibiki - Attack 2 Sheath") || asi.IsName ("Hibiki - Attack 3") || asi.IsName ("Hibiki - Attack 3") || asi.IsName ("Hibiki - Running Slash")|| asi.IsName ("Hibiki - Running Slash End"))
		{
			return true;
		}
		else return false;
	}
	
	public bool IsRunning()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);

		if(asi.IsName ("Hibiki - Running") || asi.IsName ("Hibiki - StartRunning"))
		{
			return true;
		}
		else return false;
	}

	public bool IsRunningSlash()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName ("Hibiki - Running Slash"))
		{
			return true;
		}
		else return false;
	}

	public bool IsDashAttacking()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName ("Hibiki - Dash Attack"))
		{
			return true;
		}
		else return false;
	}

	public bool IsJumping()
	{
		AnimatorStateInfo asi = animCtrl.GetCurrentAnimatorStateInfo(0);
		
		if(asi.IsName ("Hibiki - Jumping"))
		{
			return true;
		}
		else return false;
	}

	public void PerformJump()
	{
		if(animCtrl.GetBool ("OnFloor") == true && !IsAttacking ())
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
		if(attackChargePressTime < attackChargeDelay)
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
		if(!IsAttacking())
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
