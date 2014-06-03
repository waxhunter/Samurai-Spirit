using UnityEngine;
using System.Collections;

public class GroundCollider : MonoBehaviour {

	public Animator playerAC;
	public AudioSource playerAS;
	public AudioClip reachFloorClip;

	void OnTriggerEnter2D(Collider2D other) 
	{
			if(other.gameObject.name == "Player")
			{
				if(playerAC.GetBool ("OnFloor") == false)
				{
					playerAC.SetBool ("OnFloor", true);
					playerAS.clip = reachFloorClip;
					playerAS.Play();
				}
			}

	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if(other.gameObject.name == "Player")
		{
			if(playerAC.GetBool ("OnFloor") == true)
			{
				playerAC.SetBool ("OnFloor", false);
			}
		}
		
	}
}
