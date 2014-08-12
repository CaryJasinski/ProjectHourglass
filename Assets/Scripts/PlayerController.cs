using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float acceleration = 0.5f;
	public float maxVelocity = 8;
	public float lookSpeed = 10;
	public float jumpSpeed = 8;
	public float upCastLength = 6;

	private Rigidbody rigidPlayer;
	private Vector3 playerDirection = Vector3.zero;
	private Vector3 playerVelocity = Vector3.zero;
	private Vector3 expectedPosition = Vector3.zero;
	private Vector3 gravity = Vector3.zero;
	private float distanceToGround = 0; 
	private float clearence = 0; 

	public float Clearence { get { return clearence; } }
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(new Ray(expectedPosition, -Vector3.up*distanceToGround));
	}

	void Start () 
	{
		rigidPlayer = transform.rigidbody;
		distanceToGround = collider.bounds.extents.y;
		gravity = Physics.gravity;
	}

	void FixedUpdate () 
	{
		DetectInput();
		CalculateVelocity();
		ApplyVelocity();
		CalculateClearence();
	}

	void DetectInput()
	{
		playerDirection = DetermineDirection();

		//TODO: Debug why jump isn't working
		if(Input.GetKeyDown (KeyCode.Space))
			playerVelocity.y = jumpSpeed;
	}

	Vector3 DetermineDirection()
	{
		Vector3 tempDirection = Vector3.zero;

		if(Input.GetKey(KeyCode.W))
			tempDirection += new Vector3(0, 0, 1);
		if(Input.GetKey(KeyCode.A))
			tempDirection += new Vector3(-1, 0, 0);
		if(Input.GetKey(KeyCode.S))
			tempDirection += new Vector3(0, 0, -1);
		if(Input.GetKey(KeyCode.D))
			tempDirection += new Vector3(1, 0, 0);

		if(tempDirection != Vector3.zero)
		{
			tempDirection.Normalize();
			ChangeLookDirection(tempDirection);
		}

		return tempDirection;
	}

	void ChangeLookDirection (Vector3 lookDirection)
	{
		transform.rotation = Quaternion.Lerp (transform.rotation,  Quaternion.LookRotation(lookDirection), Time.fixedDeltaTime * lookSpeed);
	}
	
	void CalculateVelocity ()
	{
		playerVelocity = rigidPlayer.velocity;
		if(playerVelocity.magnitude < maxVelocity)
			playerVelocity += playerDirection * acceleration;
		//else
			//playerVelocity = playerDirection * maxVelocity;

		playerVelocity += gravity * Time.fixedDeltaTime;
	}

	void ApplyVelocity ()
	{
		if(CanMoveTo())
			rigidPlayer.velocity = playerVelocity;
		else
			rigidPlayer.velocity = Vector3.zero;
	}

	bool CanMoveTo ()
	{
		expectedPosition = playerDirection/2 + transform.position;
		return Physics.Raycast(expectedPosition, -Vector3.up, distanceToGround + 2);
	}

	void CalculateClearence ()
	{
		RaycastHit hit;
		Ray upRay = new Ray(transform.position, Vector3.up * upCastLength);
		if (Physics.Raycast(upRay, out hit))
			clearence = hit.distance;
		else
			clearence = upCastLength;
	}
}
