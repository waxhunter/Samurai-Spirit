using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {

	public string AttackButton;
	public string BlockButton;
	public string JumpButton;
	public string HorizontalAxis;
	public string VerticalAxis;

	public PlayerActionController actionCtrl;

	// Use this for initialization
	void Start () {

	}

	public bool MovingHorizontal()
	{
		if(Mathf.RoundToInt(Input.GetAxis (HorizontalAxis)) != 0)
			return true;
		else 
			return false;
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown (JumpButton))
			actionCtrl.PerformJump();

		else if(Input.GetButtonDown (BlockButton))
			actionCtrl.PerformBlock();

		else if(Input.GetButtonUp (BlockButton))
			actionCtrl.StopBlock();

		else if(Input.GetButton (AttackButton))
			actionCtrl.PerformAttackCharge(Mathf.RoundToInt(Input.GetAxis (HorizontalAxis)));

		else if(Input.GetButtonUp (AttackButton))
			actionCtrl.PerformAttack ();

		if(MovingHorizontal())
			actionCtrl.PerformMovement(Mathf.RoundToInt(Input.GetAxis (HorizontalAxis)));

		else
			actionCtrl.StopMovement();

	}
}
