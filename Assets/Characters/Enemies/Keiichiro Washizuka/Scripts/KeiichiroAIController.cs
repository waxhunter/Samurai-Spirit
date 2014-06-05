 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeiichiroAIController : MonoBehaviour {
	
	public AudioSource audioSrc1;
	public AudioSource audioSrc2;
	public Animator animCtrl;

	public float chasingDistance;
	public float attackRange;

	public float moveSpeed;

	public AudioClip TakeHitFX;

	public List<AudioClip> TakeHitVoices;

	public List<GameObject> BloodSprites;

	public GameObject player;

	public GameObject sprite;

	public float spriteAdjustValue;

	int direction = -1;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player Hit Area")
		{
			if(animCtrl.GetBool ("Hit Taken") == false)
			{
				animCtrl.SetBool ("Hit Taken", true);
				audioSrc1.clip = TakeHitVoices[Random.Range (0, TakeHitVoices.Count)];
				audioSrc1.Play();
				audioSrc2.clip = TakeHitFX;
				audioSrc2.Play();
				BloodSprites[Random.Range (0, BloodSprites.Count)].SetActive(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{

	}

	// Use this for initialization
	void Start () {
	
	}

	void setVelocity(float speed)
	{
		rigidbody2D.velocity = new Vector2((float) ( direction * speed ), rigidbody2D.velocity.y);
	}
	
	// Update is called once per frame
	void Update () {

		//
		float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);

		if(distanceToPlayer < chasingDistance && distanceToPlayer > attackRange && animCtrl.GetBool ("Attack") == false)
		{
			animCtrl.SetBool ("Running", true);
		}
		else
		{
			animCtrl.SetBool ("Running", false);

			if(distanceToPlayer <= attackRange && animCtrl.GetBool ("Attack") == false)
			{
				animCtrl.SetBool ("Attack", true);
			}
		}

		if(animCtrl.GetBool ("Running") == true)
			setVelocity(moveSpeed);
		else
			setVelocity(0f);

		if(Mathf.Abs(player.transform.position.x - this.transform.position.x) > spriteAdjustValue)
		{
			if(player.transform.position.x > this.transform.position.x && direction == -1)
			{
				direction = 1;
				sprite.transform.localPosition += new Vector3( spriteAdjustValue, 0f, 0f);
			}
			else if(player.transform.position.x < this.transform.position.x && direction == 1)
			{
				direction = -1;
				sprite.transform.localPosition -= new Vector3( spriteAdjustValue, 0f, 0f);
			}
		}
		transform.localScale = new Vector3( (float) -direction, transform.localScale.y, 0);
	}
}
