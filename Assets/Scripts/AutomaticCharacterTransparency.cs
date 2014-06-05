using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomaticCharacterTransparency : MonoBehaviour {

	public List<GameObject> affectedObjects;

	List<GameObject> objectsInside = new List<GameObject>();

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

	void Start()
	{
	}

	bool IsCharacter(GameObject obj)
	{
		if(obj.tag == "Player" || obj.tag == "Character" || obj.tag == "Enemy")
			return true;
		else
			return false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(IsCharacter (other.gameObject))
		{
			if(objectsInside.Count == 0)
			{
				fadeTransparency = TRANSPARENCY_IN;
				isTransparent = true;
				keepTransparent = true;
			}

			if(!objectsInside.Contains(other.gameObject)) objectsInside.Add (other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(IsCharacter (other.gameObject))
		{
			if(objectsInside.Contains(other.gameObject)) objectsInside.Remove (other.gameObject);

			if(objectsInside.Count == 0)
			{
					keepTransparent = false;
			}
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
