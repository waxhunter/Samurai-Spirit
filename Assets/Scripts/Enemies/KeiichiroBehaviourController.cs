 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeiichiroBehaviourController : MonoBehaviour {

	public KeiichiroActionController actionCtrl;
	public KeiichiroPhysicsController physicsCtrl;

	public AudioSource audioSrc1;
	public AudioSource audioSrc2;

	public float chasingDistance;
	public float attackRange;

	public AudioClip TakeHitFX;

	public List<AudioClip> TakeHitVoices;

	public List<GameObject> BloodSprites;

	public GameObject player;

	public GameObject sprite;

	public float spriteAdjustValue;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player Hit Area")
		{
			if(!actionCtrl.IsPerformingAction("Hit Taken"))
			{
				actionCtrl.PerformAction("Hit Taken");
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


	
	// Update is called once per frame
	void Update () {

		float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);

		if(distanceToPlayer < chasingDistance && distanceToPlayer > attackRange && !actionCtrl.IsAttacking())
		{
			actionCtrl.PerformRun();
		}
		else
		{
			actionCtrl.StopRun ();

			if(distanceToPlayer <= attackRange && !actionCtrl.IsAttacking())
			{
				actionCtrl.PerformAttack();
			}
		}



		if(Mathf.Abs(player.transform.position.x - this.transform.position.x) > spriteAdjustValue)
		{
			if(player.transform.position.x > this.transform.position.x && physicsCtrl.direction == -1)
			{
				physicsCtrl.direction = 1;
				sprite.transform.localPosition += new Vector3( spriteAdjustValue, 0f, 0f);
			}
			else if(player.transform.position.x < this.transform.position.x && physicsCtrl.direction == 1)
			{
				physicsCtrl.direction = -1;
				sprite.transform.localPosition -= new Vector3( spriteAdjustValue, 0f, 0f);
			}
		}
	}
}
