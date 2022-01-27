using TMPro;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    #region Components
    private Player player;

    // eliminar esta parte de la ui, solo para testing
    [SerializeField] public TMP_Text comboNumber;
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
        comboNumber.text = "Combo: " + player.ComboAttack.ToString();
    }

    #region Attacking
    private void OnSimpleAttack()
    {
        // it works as a cooldown, it resets on StopAttack
        if (player.IsAttacking) return;

        player.IsAttacking = true;

        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                                (Vector2)transform.position + player.Direction * player.AttackRange,
                                player.AttackRadius, player.AttackLayer);

        foreach (Collider2D collider2d in hitEnemies)
        {
            if (collider2d.CompareTag("Enemy"))
            {
                // Debug.Log("Le pegaste a " + collider2d.name);
                // change fixed value
                if (player.ComboAttack == 2)
                {
                    collider2d.GetComponent<Enemy>().TakeDamage(player.AttackDamage * 2);
                }
                else
                {
                    collider2d.GetComponent<Enemy>().TakeDamage(player.AttackDamage);
                }
            }
        }

        // stops attack
        Invoke("StopAttack", player.AttackDuration);
    }

    private void StopAttack()
    {
        player.IsAttacking = false;
        player.ComboAttack += 1;
        comboNumber.text = "Combo: " + player.ComboAttack.ToString();

        // resets combo timer
        player.ComboTimer = 0;
    }
    #endregion
}
