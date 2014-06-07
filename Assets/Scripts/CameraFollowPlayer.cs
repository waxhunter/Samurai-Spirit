using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	public float cameraMinXBound;
	public float cameraMaxXBound;

	public float cameraYOffset;
	public float cameraXOffset;

	public float cameraYDeadZone;

	public GameObject player;
	Vector3 camtarget;

	PlayerPhysicsController physicsCtrl;

	// Use this for initialization
	void Awake() {
		physicsCtrl = player.GetComponent<PlayerPhysicsController>();
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 campos = this.transform.position;
		Vector3 playerpos = player.transform.position;
		camtarget = new Vector3(playerpos.x + (cameraXOffset * physicsCtrl.direction), campos.y, campos.z);
		if(camtarget.x > cameraMinXBound && camtarget.x < cameraMaxXBound) 
		{
			float lerpSpeed = physicsCtrl.moveSpeed / 100;
			transform.position = Vector3.Lerp(campos, camtarget, lerpSpeed * Vector3.Distance (campos, camtarget));
		}


		//float pToCamDiff = Mathf.Abs (((player.transform.position.y + cameraYOffset) - transform.position.y));
		Vector3 pos = this.camera.WorldToViewportPoint(player.transform.position);
		if(pos.y > (cameraYDeadZone + 0.5) || pos.y < (-cameraYDeadZone + 0.5))
		{
		campos = this.transform.position;
		float lerpSpeedY = 0.01f + Mathf.Abs (physicsCtrl.gameObject.rigidbody2D.velocity.y) / 200;
			
		transform.position = Vector3.Lerp(campos, new Vector3(campos.x, playerpos.y, campos.z), lerpSpeedY);
		}
	}
}
