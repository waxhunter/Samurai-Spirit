using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeiichiroAIController : MonoBehaviour {

	public AudioSource audioSrc1;
	public AudioSource audioSrc2;
	public Animator animCtrl;

	public AudioClip TakeHitFX;

	public List<AudioClip> TakeHitVoices;

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
	
	}
}
