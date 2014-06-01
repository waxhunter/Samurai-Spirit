using UnityEngine;
using System.Collections;

public class ResetCombo : MonoBehaviour {

	public Animator animController;

	public void EndCombo()
	{
		animController.SetBool ("Combo", false);
	}
}
