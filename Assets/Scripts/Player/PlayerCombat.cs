using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region Components
    private Player player;
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
    }

    #region Attacking
    private void OnSimpleAttack()
    {
        // it works as a cooldown, it resets on StopAttack
        if (player.IsAttacking) return;

        Debug.Log("Attack number: " + player.ComboAttack.ToString());

        player.IsAttacking = true;

        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                                (Vector2)transform.position + player.Direction * player.AttackRange,
                                player.AttackRadius, player.AttackLayer);

        foreach (Collider2D collider2d in hitEnemies)
        {
            if (collider2d.CompareTag("Enemy"))
            {
                Debug.Log("Le pegaste a " + collider2d.name);
            }
        }

        // stops attack
        Invoke("StopAttack", player.AttackDuration);
    }

    private void StopAttack()
    {
        player.IsAttacking = false;
        player.ComboAttack += 1;
    }
    #endregion
}
