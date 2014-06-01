using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

	public float cameraMinXBound;
	public float cameraMaxXBound;
	public GameObject player;
	Vector3 camtarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 campos = this.transform.position;
		Vector3 playerpos = player.transform.position;
		camtarget = new Vector3(playerpos.x, campos.y, campos.z);
		if(camtarget.x > cameraMinXBound && camtarget.x < cameraMaxXBound) transform.position = Vector3.Lerp(campos, camtarget, 0.05f * Vector3.Distance (campos, camtarget));
	}
}
