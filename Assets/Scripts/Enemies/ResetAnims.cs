using UnityEngine;
using System.Collections;

public class ResetAnims : MonoBehaviour {

	public Animator animCtrl;

	public void ResetAnim(string anim)
	{
		animCtrl.SetBool (anim, false);
	}

}
