using UnityEngine;
using System.Collections;

public class NavMeshController : MonoBehaviour {

	private NavMeshAgent agent;
	private Vector3 desiredPostion = Vector3.zero;

	void Start () 
	{
		agent = transform.GetComponent<NavMeshAgent>();
	}

	void Update () 
	{
		if(Input.GetKey(KeyCode.UpArrow))
			desiredPostion = transform.position + transform.forward;
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, 90f*Time.deltaTime);
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -90f*Time.deltaTime);

		agent.SetDestination(desiredPostion);
	}
}
