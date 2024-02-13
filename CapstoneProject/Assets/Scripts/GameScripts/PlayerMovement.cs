using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables for Character Movement//
    float horizontalInput;
    float moveSpeed = 5f;
    float jumpSpeed = 5f;

    bool isFacingLeft = false;
    bool isJumping = false;

    Rigidbody2D rb;

    //built-in functions//
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipCharacter();

        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

    }


    //custom functions//

    //function to flip the character
    void FlipCharacter()
    {
        if (isFacingLeft && horizontalInput > 0f || !isFacingLeft && horizontalInput < 0f)
        {
            isFacingLeft = !isFacingLeft;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    //Unity functions//
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
    }
}
