using UnityEngine;
using System.Collections;

public class PlaySounds : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip attackClip;
	public AudioClip dashClip;
	public AudioClip footstepClip;
	public AudioClip jumpClip;
	public AudioClip drawSwordClip;

	public void PlayJumpSound()
	{
		audioSource.clip = jumpClip;
		audioSource.Play ();
	}

	public void PlayDrawSwordSound()
	{
		audioSource.clip = drawSwordClip;
		audioSource.Play ();
	}

	public void PlayFootstepSound()
	{
		audioSource.clip = footstepClip;
		audioSource.Play ();
	}

	public void PlayAttackSound()
	{
		audioSource.clip = attackClip;
		audioSource.Play ();
	}
	
	public void PlayDashAttackSound()
	{
		audioSource.clip = dashClip;
		audioSource.Play();
	}
}
