using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float hight = 5f;
    private int extraJump;
    public int extraJumpValue;
    public float raydist = 0.1f;
     float maxVel = 5f;

   public bool grounded;
    bool falling;
    bool isFaceingRight = true;

    public Rigidbody2D rb;
    public Animator animator;
    GameObject groundSensor;

   public LayerMask mask;

    RaycastHit2D hit2D;
    
    
    
    // Start is called before the first frame update
    void Start()
    {

        groundSensor = GameObject.Find("GroundSensor");
        extraJump = extraJumpValue;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
       
    }

    void FixedUpdate()
    {
       

        grounded = Physics2D.OverlapCircle(groundSensor.transform.position, raydist, mask);
        Move();
    }

    void Move()
    {
       float forward = Input.GetAxisRaw("Horizontal") * speed;
      rb.velocity = new Vector2(forward * Time.deltaTime, rb.velocity.y);

        if (forward > 0 && !isFaceingRight)
        {
            Flip();
        }
        else if (forward < 0 && isFaceingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(forward));
    }

    void Jump()
    {

        if (grounded == true)
        {
            extraJump = extraJumpValue;
            animator.SetBool("IsGrounded" ,true);
        }
        else if(grounded == false)
        {
            animator.SetBool("IsGrounded", false);
        }

        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            rb.AddForce(new Vector2(0f, hight), ForceMode2D.Impulse);
            extraJump--;
            
        }
        else if (Input.GetButtonDown("Jump") && extraJump == 0 && grounded == true)
        {
            rb.AddForce(new Vector2(0f, hight), ForceMode2D.Impulse);
            
        }

        if (rb.velocity.y >= maxVel)
        {
            rb.velocity = new Vector2(0f,maxVel);
        }
        
    }

    void Flip()
    {
        isFaceingRight = !isFaceingRight;
        transform.Rotate(0f, 180f, 0);
    }
}
