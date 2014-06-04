using UnityEngine;
using System.Collections;

public class ComboManager : MonoBehaviour {

	public Animator animController;

	public void EndCombo()
	{
		animController.SetBool ("Combo", false);
	}
}
