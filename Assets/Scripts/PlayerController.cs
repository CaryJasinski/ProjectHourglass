using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float acceleration = 0.5f;
	public float maxVelocity = 8;
	public float lookSpeed = 10;
	public float jumpSpeed = 8;
	public float gravity = -18;
	public float fallDistance = 50;
	public float maxCameraHeight = 6;

	private Rigidbody rigidPlayer;
	private Vector3 playerDirection = Vector3.zero;
	private Vector3 previousDirection = Vector3.zero;
	private Vector3 playerVelocity = Vector3.zero;
	private Vector3 expectedPosition = Vector3.zero;
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
	}

	void FixedUpdate () 
	{
		DetectInput();
		CalculateVelocity();
		if(isGrounded() && Input.GetKeyDown (KeyCode.Space))
		{
			playerVelocity.y = jumpSpeed;
		}
		ApplyVelocity();
		CalculateClearence();
	}

	void DetectInput()
	{
		playerDirection = DetermineDirection();

		//TODO: Debug why jump isn't working
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
			previousDirection = tempDirection;
			ChangeLookDirection(tempDirection);
		}
		else 
			ChangeLookDirection(previousDirection);

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

		playerVelocity.y += gravity * Time.fixedDeltaTime;
//		if(jumpOffset > 0)
//			jumpOffset += gravity.y * Time.fixedDeltaTime;
	}

	void ApplyVelocity ()
	{
		if(CanMoveTo())
			rigidPlayer.velocity = playerVelocity;
		else
			rigidPlayer.velocity = Vector3.zero;
	}

	bool isGrounded ()
	{
		return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f);
	}

	bool CanMoveTo ()
	{
		expectedPosition = playerDirection/2 + transform.position;
		return Physics.Raycast(expectedPosition, -Vector3.up, distanceToGround + fallDistance);
	}

	void CalculateClearence ()
	{
		RaycastHit hit;
		Ray upRay = new Ray(transform.position, Vector3.up * maxCameraHeight);
		if (Physics.Raycast(upRay, out hit))
			clearence = hit.distance;
		else
			clearence = maxCameraHeight;
	}
}
