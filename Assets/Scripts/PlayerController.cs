using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject playerSprite;
	public Animator playerAnimator;
	public AudioSource audioSource;

	public string AttackButton;
	public string BlockButton;
	public string JumpButton;

	public string HorizontalAxis;
	public string VerticalAxis;
	
	public float jumpForce;
	public float moveSpeed;
	public float fullSpeedAccelTime;

	public int attackChargeDelay;

	int direction = 1;
	int attackChargePressTime = 0;

	// Use this for initialization
	void Start () {

	}

	bool MovingHorizontal()
	{
		if(Mathf.RoundToInt(Input.GetAxis (HorizontalAxis)) != 0)
			return true;
		else 
			return false;
	}

	public void ApplyJumpPhysics()
	{
		this.rigidbody2D.AddForce(new Vector2(0, jumpForce));
	}

	bool IsAttackingAnim(AnimatorStateInfo asi)
	{
		if(asi.IsName ("Hibiki - Attack1") || asi.IsName ("Hibiki - Attack 2") || asi.IsName ("Hibiki - Attack 2 Sheath") || asi.IsName ("Hibiki - Attack 3") || asi.IsName ("Hibiki - Attack 3") || asi.IsName ("Hibiki - Running Slash")|| asi.IsName ("Hibiki - Running Slash End"))
		{
			return true;
		}
		else return false;
	}

	bool IsRunningAnim(AnimatorStateInfo asi)
	{
		if(asi.IsName ("Hibiki - Running") || asi.IsName ("Hibiki - StartRunning"))
		{
			return true;
		}
		else return false;
	}

	void setVelocity(float speed)
	{
		rigidbody2D.velocity = new Vector2((float) ( direction * speed), rigidbody2D.velocity.y);
	}

	// Update is called once per frame
	void Update () {

		AnimatorStateInfo asi = playerAnimator.GetCurrentAnimatorStateInfo( 0 );

		if(Input.GetButtonDown (JumpButton))
		{
			if(playerAnimator.GetBool ("OnFloor") == true && !IsAttackingAnim (asi))
			{
				ApplyJumpPhysics();
			}

			playerAnimator.SetTrigger("Jump");
		}
		else if(Input.GetButtonUp (BlockButton))
		{
			if(playerAnimator.GetBool ("Blocking") == true)
			{
				playerAnimator.SetBool ("Blocking", false);
			}
		}
		else if(Input.GetButton (AttackButton))
		{
			if(attackChargePressTime < attackChargeDelay)
			{
				attackChargePressTime += (int) (1000f * Time.deltaTime);
			}
			else
			{
				playerAnimator.SetBool ("ChargeAttack", true);

				if(MovingHorizontal () && playerAnimator.GetBool("ChargeAttack") == true && !IsAttackingAnim(asi) && !playerAnimator.GetBool ("DashAttack"))
				{
					direction = Mathf.RoundToInt(Input.GetAxis (HorizontalAxis));
					playerAnimator.SetTrigger ("DashAttack");
				}
				else
				{
					playerAnimator.SetBool ("DashAttack", false);
				}
			}
		}
		else if(Input.GetButtonUp (AttackButton))
		{
			if(playerAnimator.GetBool ("ChargeAttack") == true)
			{
				playerAnimator.SetBool ("ChargeAttack", false);
				attackChargePressTime = 0;
			}
			else
			{
				if(IsAttackingAnim(asi) || asi.IsName ("Hibiki - Idle" ) || asi.IsName ("Hibiki - Running" ))
				{
					playerAnimator.SetBool("Combo", true);
				}
			}
		}
		if(Input.GetButton (BlockButton))
		{
			playerAnimator.SetBool ("Blocking", true);
		}
		else
		{

			if(MovingHorizontal() && !IsAttackingAnim(asi))
			{
				playerAnimator.SetBool("Running", true);
				direction = Mathf.RoundToInt(Input.GetAxis (HorizontalAxis));
			}

			else
			{
				playerAnimator.SetBool("Running", false);
			}
		}

		if(IsRunningAnim(asi))
		{
			setVelocity(moveSpeed);
		}
		else if(asi.IsName ("Hibiki - Running Slash") )
		{
			setVelocity(moveSpeed * 2f);
		}
		else if(asi.IsName ("Hibiki - Dash Attack") )
		{
			setVelocity(moveSpeed * 2f);
		}
		else if((asi.IsName ("Hibiki - Jumping") || asi.IsName ("Hibiki - Jumping Reach Floor")) && playerAnimator.GetBool ("Running") == true )
		{
			setVelocity(moveSpeed);
		}
		else
		{
			setVelocity(0f);
		}

		transform.localScale = new Vector3(direction, 1, 1);
	}
}
