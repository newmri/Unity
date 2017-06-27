using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float MoveSpeed = 5.0f;
	public float RotateSpeed = 500.0f;
	public float VerticalSpeed = 0.0f;
	private float gravity = 9.8f;
	
	private CharacterController charactercontroller;
	
	private Vector3 MoveDirection = Vector3.zero;
	private CollisionFlags collisionflags;
	
	public AnimationClip idleAnim;
	public AnimationClip walkAnim;
	public AnimationClip attackAnim;
	public AnimationClip skillAnim;
	public enum CharacterState
	{
		IDLE = 0,
		WALK = 1,
		ATTACK = 2,
		SKILL = 3,
		SIZE
	}
	private CharacterState state = CharacterState.IDLE;	
	
	// Use this for initialization
	void Start () {
		charactercontroller = GetComponent<CharacterController>();
		
		animation.wrapMode = WrapMode.Loop;
		animation.Stop();
		
		animation[attackAnim.name].wrapMode = WrapMode.Once;
		animation[attackAnim.name].layer = 1;

		animation[skillAnim.name].wrapMode = WrapMode.Once;
		animation[skillAnim.name].layer = 1;

		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		CheckState();
		AnimationControl();
		BodyDirection();
		ApplyGravity();
	}

	void ApplyGravity()
	{
		if(charactercontroller.isGrounded == true)
		{
			VerticalSpeed = 0.0f;
		}else
		{
			//on air
			VerticalSpeed -= gravity * Time.deltaTime;
		}
	}
	
	void BodyDirection()
	{
		Vector3 horizontalVelocity = charactercontroller.velocity;
		horizontalVelocity.y = 0.0f;
		if(horizontalVelocity.magnitude > 0.0f)
		{
			Vector3 trans = horizontalVelocity.normalized;
			Vector3 wantedVector = Vector3.Lerp(transform.forward,
			                                    trans,0.5f);
			if(wantedVector != Vector3.zero)
			{
				transform.forward = wantedVector;
			}
		}
	}

	void AnimationControl()
	{
		switch(state)
		{
		case CharacterState.IDLE:
			animation.CrossFade(idleAnim.name);
			break;
		case CharacterState.WALK:
			animation.CrossFade(walkAnim.name);
			break;
		case CharacterState.ATTACK:
			if(animation[attackAnim.name].normalizedTime > 0.9f)
			{
				animation[attackAnim.name].normalizedTime = 0.0f;
				state = CharacterState.IDLE;
			}else
			{
				animation.CrossFade(attackAnim.name);
			}
			break;
		case CharacterState.SKILL:
			if(animation[skillAnim.name].normalizedTime > 0.9f)
			{
				animation[skillAnim.name].normalizedTime = 0.0f;
				state = CharacterState.IDLE;
			}else
			{
				animation.CrossFade(skillAnim.name);
			}
			break;
		}

	}
	
	void CheckState()
	{
		if(state == CharacterState.ATTACK||state == CharacterState.SKILL) 
		{
			return;
		}
		
		if(charactercontroller.velocity.sqrMagnitude > 0.1f)
		{
			//move
			state = CharacterState.WALK;
		}else
		{
			//stand
			state = CharacterState.IDLE;
		}
		if(Input.GetMouseButtonDown(0))
		{
			state = CharacterState.ATTACK;
		}
		if(Input.GetMouseButtonDown(1))
		{
			state = CharacterState.SKILL;
		}

	}
	
	void Move()
	{
		Transform cameraTransform = Camera.mainCamera.transform;
		
		Vector3 forward = cameraTransform.
			TransformDirection(Vector3.forward);
		forward = forward.normalized; //normal vector 1 vector
		Vector3 right = new Vector3(forward.z ,0.0f,-forward.x);
		
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		
		Vector3 targetVector = v *forward + h * right;
		targetVector = targetVector.normalized; //normal vector
		
		MoveDirection = Vector3.RotateTowards(MoveDirection,
		                                      targetVector,RotateSpeed*Mathf.Deg2Rad*Time.deltaTime
		                                      ,500.0f);
		MoveDirection = MoveDirection.normalized;
		
		Vector3 grav = new Vector3(0.0f,VerticalSpeed,0.0f);
		
		Vector3 movementAmt = (MoveDirection * MoveSpeed * 
		                       Time.deltaTime)+grav;
		collisionflags = charactercontroller.Move(movementAmt);
		
	}
}
