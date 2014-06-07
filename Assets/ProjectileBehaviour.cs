using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

	Vector3 originalPosition;
	public GameObject launcher;

	PhysicsController physicsCtrl;

	public float speed;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name != "Enemy" && !other.isTrigger && !Physics2D.GetIgnoreLayerCollision(other.gameObject.layer, this.gameObject.layer))
		{
			//this.collider2D.enabled = false;
			this.transform.localPosition = originalPosition;
			this.gameObject.SetActive(false);
			//this.collider2D.enabled = true;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{

	}

	void Awake()
	{
		physicsCtrl = launcher.GetComponent<PhysicsController>();
	}

	// Use this for initialization
	void Start () {

		originalPosition = this.transform.localPosition;;

	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.AddForce (new Vector2(physicsCtrl.direction * speed, 0f));
	}
}
