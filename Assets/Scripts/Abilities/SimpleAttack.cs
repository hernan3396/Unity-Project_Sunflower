using UnityEngine;

[CreateAssetMenu]
public class SimpleAttack : Ability
{
    public override void Activate(Transform target)
    {
        // this is for showing attack effect radius
        // simple implementation, may change later
        target.localScale = new Vector3(radius, radius, radius) * 2;
        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(target.position, radius, layer);

        foreach (Collider2D collider2d in hitEnemies)
        {
            if (collider2d.CompareTag("Enemy"))
            {
                collider2d.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
