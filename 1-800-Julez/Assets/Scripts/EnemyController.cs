using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Transform lPoint, rPoint;
    private bool moveRight;
    public Rigidbody2D theRB;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        lPoint.parent = null;
        rPoint.parent = null;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveRight) {
            
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

            if (transform.position.x > rPoint.position.x)
            {
                moveRight = false;
            }

        } else
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

            if (transform.position.x < lPoint.position.x)
            {
                moveRight = true;
            }
        }
    }
}
