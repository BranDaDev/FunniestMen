using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;

    private TrailRenderer tr;

    public float jumpforce;
    private bool isGrounded;
    private bool canDoubleJump;

    private bool canDash;
    private bool isDashing;
    private float dashPower = 46f;
    private float dashTime = 0.15f;
    private Vector2 dashDir;
    private float dashCooldown = 1f;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private Animator anim;
    private SpriteRenderer theSR;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();

        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            theRB.velocity = dashDir.normalized * dashPower;
            return;
        }

        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (isGrounded == true)
        {
            canDoubleJump = true;
            canDash = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpforce);
            }

            else
            {
                if (canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpforce);
                    canDoubleJump = false;
                }
            }
        }
        if (Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash());
        }

        if(theRB.velocity.x < 0)
        {
        theSR.flipX = true;
        } else if(theRB.velocity.x > 0)
        {
            theSR.flipX = false;
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }
    private IEnumerator Dash()
    {
        isGrounded = false;
        canDash = false;
        isDashing = true;
        tr.emitting = true;
        dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dashDir == Vector2.zero)
        {
            dashDir = new Vector2(transform.localScale.x, 0f);
        }

        float gravity = theRB.gravityScale;
        theRB.velocity = new Vector2(dashDir.x * dashPower, dashDir.y * dashPower);
        yield return new WaitForSeconds(dashTime);
        theRB.gravityScale = gravity;
        isDashing = false;
        tr.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
    } 
  
}
   
    
