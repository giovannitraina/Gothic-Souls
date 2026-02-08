using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private float JumpForce = 400f;                          
	[Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;          
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  
	[SerializeField] private bool AirControl = false;                         
	[SerializeField] private LayerMask GroundLayer;                          
	[SerializeField] private Transform GroundCheck;                           
	[SerializeField] private Transform CeilingCheck;                         
	[SerializeField] private Collider2D CrouchDisableCollider;               

	const float GroundedRadius = .22f; 
	public bool IsGrounded;            
	const float CeilingRadius = .05f; 
	private Rigidbody2D myRigidbody2D;
	private bool IsFacingRight = true; 
	private Vector3 Velocity = Vector3.zero;

	[System.Serializable]

	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void Update()
	{
		IsGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, GroundLayer);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				IsGrounded = true;
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		if (!crouch)
		{
			if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, GroundLayer))
			{
				crouch = true;
			}
		}

		if (IsGrounded || AirControl)
		{

			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				move *= CrouchSpeed;

				if (CrouchDisableCollider != null)
					CrouchDisableCollider.enabled = false;
			}
			else
			{
				if (CrouchDisableCollider != null)
					CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			Vector3 targetVelocity = new Vector2(move * 10f, myRigidbody2D.velocity.y);

			myRigidbody2D.velocity = Vector3.SmoothDamp(myRigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

			if (move > 0 && !IsFacingRight)
			{

				Flip();
			}

			else if (move < 0 && IsFacingRight)
			{
				Flip();
			}
		}

		if (IsGrounded && jump)
		{
			IsGrounded = false;
			myRigidbody2D.AddForce(new Vector2(0f, JumpForce));
		}
	}


	private void Flip()
	{
		IsFacingRight = !IsFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}