using UnityEngine;
using System.Collections;

public class EffectActivator : MonoBehaviour {

	public EffectsController effectsCtrl;

	public void ActivateGraphicEffect(string effect)
	{
		effectsCtrl.ActivateGraphicEffect(effect);
	}

	public void PlaySoundEffect(string effect)
	{
		effectsCtrl.PlaySoundEffect(effect);
	}
}
