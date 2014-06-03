using UnityEngine;
using System.Collections;

public class ObjectDeactivator : MonoBehaviour {

	bool deactivateOnNextFrame = false;

	public void SetInactive()
	{
		deactivateOnNextFrame = true;
	}

	void Update()
	{
		if(deactivateOnNextFrame)
		{
			deactivateOnNextFrame = false;
			this.gameObject.SetActive(false);
		}
	}
}
