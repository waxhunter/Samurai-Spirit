using UnityEngine;
using System.Collections;

public class KeichiiroResetAnims : MonoBehaviour {

	public Animator animCtrl;

	public void ResetAnims()
	{
		animCtrl.SetBool ("Hit Taken", false);
	}
}
