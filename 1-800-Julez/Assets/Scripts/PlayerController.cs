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
    private float dashTime = 0.1f;
    private Vector2 dashDir;
    private float dashCooldown = 1f;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {

        tr = GetComponent<TrailRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
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
    }
    private IEnumerator Dash()
    {
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
        gravity = theRB.gravityScale;
        theRB.velocity = new Vector2(transform.localScale.x * dashPower, transform.localScale.y * dashPower);
        yield return new WaitForSeconds(dashTime);
        theRB.gravityScale = gravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
   
    