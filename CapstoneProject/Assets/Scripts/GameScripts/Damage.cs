using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 0;
    public Enemy enemy;
    public PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !player.isBlocking)
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
        else if (collision.CompareTag("Enemy") && !enemy.isBlocking)
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
