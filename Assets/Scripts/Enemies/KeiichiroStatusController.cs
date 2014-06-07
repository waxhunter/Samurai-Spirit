using UnityEngine;
using System.Collections;

public class KeiichiroStatusController : MonoBehaviour {

	public KeiichiroActionController actionCtrl;

	public float health;

	public void TakeDamage(float damage)
	{
		health -= damage;
		if(health < 0f)
			actionCtrl.Die();
	}
}
