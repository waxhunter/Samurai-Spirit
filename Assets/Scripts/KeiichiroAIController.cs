using UnityEngine;
using System.Collections;

public class KeiichiroAIController : MonoBehaviour {

	public Animator animCtrl;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player Hit Area")
		{
			animCtrl.SetBool ("Hit Taken", true);
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
