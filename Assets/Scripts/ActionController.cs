using UnityEngine;
using System.Collections;

public class ActionController : MonoBehaviour {

	public Animator animCtrl;

	public bool IsPerformingAction(string action)
	{
		if(animCtrl.GetBool (action) == true)
			return true;

		else
			return false;
	}

	public void PerformAction(string action)
	{
		animCtrl.SetBool (action, true);
	}

	public void StopAction(string action)
	{
		animCtrl.SetBool (action, false);
	}
}
