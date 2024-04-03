using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAnimations : StateMachineBehaviour
{

    public float walkSpeed = 3f;
    public float runSpeed = 4f;
    public float attackRange = 3f;
    public float jumpHeight = 12f;

    private int jumpsLeft = 0;
    private int maxJumps = 2;

    public float minJump = 5f;
    public float maxJump = 10f;
    private float nextJump = 0f;

    public bool isJumping = false;

    public Timer timerScript;

    Transform Player;
    Rigidbody2D rb;
    Enemy enemy;

    bool isGrounded;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (timerScript == null)
        {
            timerScript = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        }

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (timerScript.countdown > 0)
        {
            return;
        }

        Vector2 target = new Vector2(Player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, runSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(Player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Jab");
        }

        //if (timerScript.countdown <= 0 && Time.time >= nextJump)
        //{
        //    EnemyJump(animator);
        //    nextJump = Time.time + UnityEngine.Random.Range(minJump, maxJump);
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isGrounded = collision.gameObject.CompareTag("Ground");
    //}

    //public void EnemyJump(Animator animator)
    //{
    //    Debug.Log("EnemyJump called, isGrounded: " + isGrounded);
    //    isGrounded = false;
    //    rb.AddForce(Vector2.up * jumpHeight);
    //    animator.SetBool("isJumping", true);
    //}
}
