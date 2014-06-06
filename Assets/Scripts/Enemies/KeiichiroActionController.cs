using UnityEngine;
using System.Collections;

public class KeiichiroActionController : ActionController {

	public bool IsRunning()
	{
		if(animCtrl.GetBool ("Running") == true)
			return true;

		else 
			return false;
	}

	public bool IsAttacking()
	{
		if(animCtrl.GetBool ("Attack") == true)
			return true;
		
		else 
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
}
