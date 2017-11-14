using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10, jumpVelocity = 10;
    public LayerMask playerMask;
    Transform myTrans, tagGround, tagLeft, tagRight;
    Rigidbody2D myBody;

    public GameObject bulletToRight, bulletToLeft;
    Vector2 bulletPos;
    public float fireRate = 0.5f;
    float nextFire = 0.0f;
	private Vector3 _target;
	private Animator playerAnimator;

	bool isGrounded = false, isLeftSide = false, isRightSide = false, jumping = false, dashIsCalled = false;
    public bool facingRight = false, facingLeft = false;

    // Use this for initialization
    void Start()
    {

        myBody = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;
        tagLeft = GameObject.Find(this.name + "/tag_leftSide").transform;
        tagRight = GameObject.Find(this.name + "/tag_rightSide").transform;
        
        
    }

	void Update()
	{

		_target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		_target.z = 0;
		float input_x = Input.GetAxisRaw("Horizontal");
		float input_y = Input.GetAxisRaw("Vertical");

		bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) > 0;

		playerAnimator.SetBool("isWalking", isWalking);
		if(isWalking)
		{

			playerAnimator.SetFloat("x", input_x);
			playerAnimator.SetFloat("y", input_y);



		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);
        isLeftSide = Physics2D.Linecast(myTrans.position, tagLeft.position, playerMask);
        isRightSide = Physics2D.Linecast(myTrans.position, tagRight.position, playerMask);


		playerAnimator.SetBool ("isAtking", false);
   




			Move (Input.GetAxisRaw ("Horizontal"));


        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            facingLeft = false;
            facingRight = true;

        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            facingLeft = true;
            facingRight = false;


        }

        if (Input.GetButtonDown("Jump"))
        {
			
            jumping = true;
            Jump();
        }

		if (isGrounded == false) {
			playerAnimator.SetBool ("isJumping", true);
		}
		if (isGrounded == true) {
			playerAnimator.SetBool ("isJumping", false);
		}








        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
			playerAnimator.SetBool ("isAtking", true);
			nextFire = Time.time + fireRate;
            Fire();
            

        }

    }

    public void Move(float horizonalInput)
    {
        Vector2 moveVel = myBody.velocity;

        if (isLeftSide && !isGrounded && jumping == false|| isRightSide && !isGrounded && jumping == false)
        {

            moveVel.y = -0.5f;

        }

            
            moveVel.x = horizonalInput * speed;
            myBody.velocity = moveVel;
       




    }

    public void Jump()
    {
		
        Vector2 moveVel = myBody.velocity;
        if (isGrounded)
        {
            myBody.velocity += jumpVelocity * Vector2.up;
            jumping = false;

        }

        if (isLeftSide && !isGrounded)
        {
            myBody.velocity += jumpVelocity * Vector2.up;
            Move(-Input.GetAxisRaw("Horizontal") * 5);
            jumping = false;


        }
        if (isRightSide && !isGrounded)
        {
            myBody.velocity += jumpVelocity * Vector2.up;
            Move(-Input.GetAxisRaw("Horizontal") * 5);
            jumping = false;

        }

    }

   
    void Fire()
    {
		var shoot = 100;

		while (shoot > 0) {
			shoot--;
		}
        bulletPos = transform.position;
		if (facingRight && shoot <= 0)
        {
            bulletPos += new Vector2(1, 0);
            Instantiate(bulletToRight, bulletPos, Quaternion.identity);

        }
		if (facingLeft && playerAnimator.GetBool("isAtking") == true && shoot <= 0)
        {
            bulletPos -= new Vector2(1, 0);
            Instantiate(bulletToLeft, bulletPos, Quaternion.identity);


        }
    }



	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("walls") && isGrounded == false) {
			playerAnimator.SetBool ("isOnWall", true);

			if (isGrounded == true) {
				playerAnimator.SetBool ("isOnWall", false);
			}
		}

		if (isGrounded == true) {
			playerAnimator.SetBool ("isOnWall", false);
		}
	}




	public void Dash(float horizonalInput)
	{
		Vector2 moveVel = myBody.velocity;
		dashIsCalled = true;
		playerAnimator.SetBool ("isAtking", true);
		if (isLeftSide && !isGrounded && jumping == false|| isRightSide && !isGrounded && jumping == false)
		{

			moveVel.y = -0.5f;

		}


		moveVel.x = horizonalInput * speed;
		myBody.velocity = moveVel;





	}
      

}


