using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Very basic movement for player. still has a lot of issues especially for jumping, sometimes dropping inputs and clinging to walls*/
public class playerMovementComponent : MonoBehaviour
{
    //Movement
    public float horizontalSpeed;
    public float jumpSpeed;

    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // vcheck if grounded
            
            if (System.Math.Abs(rigidbody.velocity.y) < 0.1)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
            }
        }

        float horizontalMoveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalMoveVelocity = -horizontalSpeed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalMoveVelocity = horizontalSpeed;
        }

        rigidbody.velocity = new Vector2(horizontalMoveVelocity, rigidbody.velocity.y);

    }
   

}
