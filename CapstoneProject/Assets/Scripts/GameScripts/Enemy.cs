using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player;
    public EnemyAnimations animations;

    public bool isFlipped = false;
    private float jumpChance = 0.1f;

    void Update()
    {
        if (Random.value < jumpChance)
        {
            animations.EnemyJump(GetComponent<Animator>());
        }
    }
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
}
