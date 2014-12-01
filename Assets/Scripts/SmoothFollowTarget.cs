using UnityEngine;
using System.Collections;

public class SmoothFollowTarget : MonoBehaviour {

	public Transform target;
	public Vector3 followOffset;
	public float followSpeed = 1;

	private Vector3 targetPosition;
	private Vector3 velocity;
	private PlayerController playerController;

	void Start ()
	{
		if(target.CompareTag("Player"))
			playerController = target.GetComponent<PlayerController>();
	}

	void LateUpdate () 
	{
		targetPosition = target.position + followOffset;
		if(target.CompareTag("Player"))
			targetPosition.y = target.position.y + playerController.Clearence - 1;
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed);
	}	
}
