using UnityEngine;

[CreateAssetMenu]
public class SimpleAttack : Ability
{
    public override void Activate(Vector2 position)
    {
        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(position, radius, layer);

        foreach (Collider2D collider2d in hitEnemies)
        {
            if (collider2d.CompareTag("Enemy"))
            {
                collider2d.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
