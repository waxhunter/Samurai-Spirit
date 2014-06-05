using UnityEngine;
using System.Collections;

public class KeichiiroResetAnims : MonoBehaviour {

	public Animator animCtrl;

	public void ResetHitAnim()
	{
		animCtrl.SetBool ("Hit Taken", false);
	}

	public void ResetAttackAnim()
	{
		animCtrl.SetBool("Attack", false);
	}
}
