using UnityEngine;
using System.Collections;

public class LockedFollowTarget : MonoBehaviour {

	public Transform target;
	public Vector3 followOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position + (target.rotation*followOffset);
	}
}
