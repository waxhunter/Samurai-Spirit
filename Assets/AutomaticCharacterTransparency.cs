using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomaticCharacterTransparency : MonoBehaviour {

	public List<GameObject> affectedObjects;

	public GameObject player;

	const int TRANSPARENCY_NORMAL = 0;
	const int TRANSPARENCY_OUT = 1;
	const int TRANSPARENCY_IN = 2;

	public float fadeTime;
	public float transparency;

	int fadeTransparency;
	bool isTransparent = false;
	float transparencyValue = 1.0f;

	bool keepTransparent = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && !isTransparent)
		{
			fadeTransparency = TRANSPARENCY_IN;
			isTransparent = true;
			keepTransparent = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && isTransparent)
		{
			keepTransparent = false;
		}
	}

	void setTransparency(float value)
	{
		foreach(GameObject obj in affectedObjects)
		{
			SpriteRenderer sprRenderer = obj.GetComponent<SpriteRenderer>();
			Color sprColor = sprRenderer.color;
			sprRenderer.color = new Color(sprColor.r,sprColor.g,sprColor.b, value);
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if(keepTransparent == false && isTransparent)
		{
			fadeTransparency = TRANSPARENCY_OUT;
		}

		if(fadeTransparency == TRANSPARENCY_OUT)
		{
			if(transparencyValue < 1.0f)
			{
				transparencyValue += transparency * Time.deltaTime / fadeTime;
				setTransparency(transparencyValue);
			}
			else
			{
				transparencyValue = 1.0f;
				fadeTransparency = TRANSPARENCY_NORMAL;
				isTransparent = false;
			}
		}
		else if(fadeTransparency == TRANSPARENCY_IN)
		{
			if(transparencyValue > transparency)
			{
				transparencyValue -= transparency * Time.deltaTime / fadeTime;
				setTransparency(transparencyValue);
			}
			else
			{
				transparencyValue = transparency;
				fadeTransparency = TRANSPARENCY_NORMAL;
			}
		}

	}
}
