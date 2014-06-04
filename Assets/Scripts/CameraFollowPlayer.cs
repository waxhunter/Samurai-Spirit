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

	// Use this for initialization
	void Awake() {

	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 campos = this.transform.position;
		Vector3 playerpos = player.transform.position;
		camtarget = new Vector3(playerpos.x + cameraXOffset, campos.y, campos.z);
		if(camtarget.x > cameraMinXBound && camtarget.x < cameraMaxXBound) transform.position = Vector3.Lerp(campos, camtarget, 0.05f * Vector3.Distance (campos, camtarget));

		float pToCamDiff = (player.transform.position.y - transform.position.y);
		if(Mathf.Abs (pToCamDiff) > cameraYDeadZone)
		{
			campos = this.transform.position;
			if(pToCamDiff > 0)
			{
				transform.position = Vector3.Lerp(campos, new Vector3(campos.x, playerpos.y + (pToCamDiff - cameraYDeadZone), campos.z), 0.025f * Mathf.Abs (campos.y - playerpos.y));
			}
			else if(pToCamDiff < 0)
			{
				transform.position = Vector3.Lerp(campos, new Vector3(campos.x, playerpos.y - (pToCamDiff + cameraYDeadZone), campos.z), 0.025f * Mathf.Abs (campos.y - playerpos.y));
			}
		}
	}
}
