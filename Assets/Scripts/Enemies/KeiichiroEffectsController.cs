using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeiichiroEffectsController : EffectsController {

	public AudioClip TakeHitFX;

	public List<AudioClip> TakeHitVoices;
	
	public List<GameObject> BloodSprites;

	public void TakeHit()
	{
		audioSources[0].clip = TakeHitVoices[Random.Range (0, TakeHitVoices.Count)];
		audioSources[0].Play();
		audioSources[1].clip = TakeHitFX;
		audioSources[1].Play();
		BloodSprites[Random.Range (0, BloodSprites.Count)].SetActive(true);
	}
}
