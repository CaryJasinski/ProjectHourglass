using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {

	public Transform target;
	
	void FixedUpdate ()
	{
		transform.LookAt(target);
	}
}
