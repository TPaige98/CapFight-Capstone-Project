using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables for Enemy Movement
    float enemyDirection;
    public float walkSpeed = 3f;
    private float blockCooldown = 0f;

    //Variables for Enemy Jumping
    public float jumpHeight = 20f;
    int jumpsLeft = 1;
    bool ableToJump = false;

    //Variables for Enemy Attacking
    public float jabRange = 2f;
    public float punchRange = 4f;

    //Variables for Enemey Attack Timing
    public int combo = 0;
    public int maxCombo = 2;

    //Variables for Checking Ground
    public LayerMask groundLayer;
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(1.45f, 0.05f);

    //Variables for Unity Objects and Scripts
    public Transform Player;
    public Timer timerScript;
    public PlayerMovement playerMoves;
    private Animator animator;
    Rigidbody2D rb;

    //Audio Sources
    [SerializeField] private AudioSource JabSoundEffect;
    [SerializeField] private AudioSource PunchSoundEffect;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = animator.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMoves = Player.GetComponent<PlayerMovement>();

        StartCoroutine(FirstEnemyJump());
    }

    void Update()
    {
        Idle();
        LookAtPlayer();
        Move();

        Jab();
        Punch();

        Debug.Log("Is blocking: " + isBlocking);
        Debug.Log("Is Grounded: " + isGrounded());
        Debug.Log("Jumps Left: " + jumpsLeft);
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("EnemyPunch") && !stateInfo.IsName("EnemyJab"))
            {
                isAttacking = false;
            }
        }

        Jump();
        Block();
    }

    // MOVEMENT -------------------------------------------------------------------------- MOVEMENT//
    public void Move()
    {
        if (timerScript.countdown <= 0 && !isAttacking && !isBlocking && isGrounded())
        {
            enemyDirection = Mathf.Sign(Player.position.x - rb.position.x);
            rb.velocity = new Vector2(enemyDirection * walkSpeed, rb.velocity.y);

            animator.SetFloat("Speed", Mathf.Abs(enemyDirection * walkSpeed));
        }
    }

    public void Idle()
    {
        if (timerScript.countdown > 0)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }

    public void Jump()
    {
        if (jumpsLeft > 0 && ableToJump)
        {
            if (timerScript.countdown <= 0 && !isAttacking && !isBlocking && isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.SetBool("isJumping", true);
                jumpsLeft--;

                StartCoroutine(WaitForJump());
            }
        }
    }

    IEnumerator FirstEnemyJump()
    {
        yield return new WaitForSeconds(8f);
        ableToJump = true;
    }

    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(9f);

        jumpsLeft = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    //DEFENSE --------------------------------------------------------------------- DEFENSE//
    public bool isBlocking = false;

    public void Block()
    {
        if (timerScript.countdown <= 0 && !isAttacking && !isBlocking && isGrounded() && blockCooldown <= 0)
        {
            if (Vector2.Distance(Player.position, rb.position) <= punchRange && playerMoves.isAttacking())
            {
                isBlocking = true;
                animator.SetBool("isBlocking", true);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                StartCoroutine(WaitForBlock());
            }
        }

        if (blockCooldown > 0)
        {
            blockCooldown -= Time.deltaTime;
        }
    }

    IEnumerator WaitForBlock()
    {
        yield return new WaitForSeconds(4f);

        isBlocking = false;
        animator.SetBool("isBlocking", false);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        blockCooldown = 4.0f;
    }

    //ATTACKS ------------------------------------------------------------------- ATTACKS//

    public bool isAttacking = false;

    public void Punch()
    {
        if (Vector2.Distance(Player.position, rb.position) <= punchRange && !isAttacking && !isBlocking && isGrounded() && combo < maxCombo)
        {
            Debug.Log("Punch Triggered");
            isAttacking = true;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Punch");
            PunchSoundEffect.Play();
            maxCombo--;

            StartCoroutine(Combo());
        }
    }

    public void Jab()
    {
        if (Vector2.Distance(Player.position, rb.position) <= jabRange && !isAttacking && !isBlocking && isGrounded() && combo < maxCombo)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Jab");
            JabSoundEffect.Play();
            maxCombo--;

            StartCoroutine(Combo());
        }
    }

    IEnumerator Combo()
    {
        if (maxCombo <= 0)
        {
            yield return new WaitForSeconds(4f);
            maxCombo = 3;
        }
    }


    //ORIENTATION ------------------------------------------------------------------- ORIENTATION//
    public bool isFlipped = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;

        if (transform.position.x > Player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = false;
        }
        else if (transform.position.x < Player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = true;
        }
    }

    //PHYSICS -------------------------------------------------------------------------- PHYSICS//

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
