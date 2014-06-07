using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectsController : MonoBehaviour {

	public List<AudioSource> audioSources;

	public List<AudioClip> soundEffects;
	public List<GameObject> graphicEffects;

	public void ActivateGraphicEffect(string effectName)
	{
		foreach(GameObject effect in graphicEffects)
		{
			if(effect.name == effectName)
			{
				effect.SetActive(true);
			}
		}
	}

	public void PlaySoundEffect(string effectName)
	{
		foreach(AudioClip effect in soundEffects)
		{
			if(effect.name == effectName)
			{
				foreach(AudioSource src in audioSources)
				{
					if(src.isPlaying == false)
					{
						src.clip = effect;
						src.Play ();
					}
				}
			}
		}
	}
}
