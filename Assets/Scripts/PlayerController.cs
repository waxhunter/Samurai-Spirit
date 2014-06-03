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

	// Update is called once per frame
	void Update () {

		AnimatorStateInfo asi = playerAnimator.GetCurrentAnimatorStateInfo( 0 );

		if(Input.GetButtonDown (JumpButton))
		{
			if(playerAnimator.GetBool ("OnFloor") == true)
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

				if(MovingHorizontal () && playerAnimator.GetBool("ChargeAttack") == true && !playerAnimator.GetBool ("DashAttack"))
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
				if(asi.IsName ("Hibiki - Attack1") || asi.IsName ("Hibiki - Attack 2") || asi.IsName ("Hibiki - Attack 2 Sheath") )
				{
					playerAnimator.SetBool("Combo", true);
				}
				else
				{
					playerAnimator.SetTrigger("Attack1");
				}
			}
		}
		if(Input.GetButton (BlockButton))
		{
			playerAnimator.SetBool ("Blocking", true);
		}
		else
		{

			if(MovingHorizontal())
			{
				playerAnimator.SetBool("Running", true);
				direction = Mathf.RoundToInt(Input.GetAxis (HorizontalAxis));
			}

			else
			{
				playerAnimator.SetBool("Running", false);
			}
		}

		if(asi.IsName ("Hibiki - Running") )
		{
			/*if(Mathf.Abs (rigidbody2D.velocity.x) < (moveSpeed - 50))
				rigidbody2D.AddForce(new Vector2( (moveSpeed * direction * Time.deltaTime * 10) , 0 ));
			else*/

				//rigidbody2D.velocity = new Vector2((float) ( direction * moveSpeed ), rigidbody2D.velocity.y);
			transform.position = Vector3.MoveTowards(transform.position, transform.position + (new Vector3(0.25f * direction, 0f, 0f)), 4.0f * Time.deltaTime);
		}
		else if(asi.IsName ("Hibiki - Running Slash") )
		{
			transform.position = Vector3.MoveTowards(transform.position, transform.position + (new Vector3(0.25f * direction, 0f, 0f)), 4.0f * Time.deltaTime);
		}
		else if(asi.IsName ("Hibiki - Dash Attack") )
		{
			transform.position = Vector3.MoveTowards(transform.position, transform.position + (new Vector3(0.25f * direction, 0f, 0f)), 6.0f * Time.deltaTime);
		}
		else if((asi.IsName ("Hibiki - Jumping") || asi.IsName ("Hibiki - Jumping Reach Floor")) && playerAnimator.GetBool ("Running") == true )
		{
			transform.position = Vector3.MoveTowards(transform.position, transform.position + (new Vector3(0.25f * direction, 0f, 0f)), 4.0f * Time.deltaTime);
		}

		transform.localScale = new Vector3(direction, 1, 1);
	}
}
