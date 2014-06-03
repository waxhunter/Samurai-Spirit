using UnityEngine;
using System.Collections;

public class HitAreasController : MonoBehaviour {
	
	public GameObject DashAttackHitArea;

	public void ActivateAttackHitArea(string objname)
	{
		GameObject.Find (objname).collider2D.enabled = true;
	}

	public void DeactivateAttackHitArea(string objname)
	{
		GameObject.Find (objname).collider2D.enabled = false;
	}
}
