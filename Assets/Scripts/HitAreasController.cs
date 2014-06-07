using UnityEngine;
using System.Collections;

public class HitAreasController : MonoBehaviour {

	public GameObject referenceObject;

	public void ActivateAttackHitArea(string objname)
	{

		Transform hitArea = referenceObject.transform.FindChild(objname);
		print (hitArea.gameObject.name);
		hitArea.gameObject.collider2D.enabled = true;
	}

	public void DeactivateAttackHitArea(string objname)
	{
		referenceObject.transform.FindChild(objname).collider2D.enabled = false;
	}
}
