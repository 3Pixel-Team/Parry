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
    public float maxVel = 5f;
    


   public bool grounded;
    bool falling;
    bool isFaceingRight = true;
    public bool jump;

    public Rigidbody2D rb;
    public Animator animator;
    GameObject groundSensor;
    CharacterStats stats;

    public LayerMask mask;

    RaycastHit2D hit2D;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterStats>();
        groundSensor = GameObject.Find("GroundSensor");
        extraJump = extraJumpValue;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && extraJump == 0 && grounded == true)
        {
            Jump();
        }

        if(rb.velocity.y < 0)
        {
            animator.SetBool("SecondJump", true);
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        if (grounded == true)
        {
            extraJump = extraJumpValue;
            animator.SetBool("IsGrounded", true);
        }
        else if (grounded == false)
        {
            animator.SetBool("IsGrounded", false);
            animator.SetBool("SecondJump", false);
        }

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
        rb.AddForce(new Vector2(0f, hight), ForceMode2D.Impulse);
        extraJump--;

        jump = true;
       
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

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Die()
    {
        if(stats.health <= 0)
        {
            //Die
        }
    }
}
