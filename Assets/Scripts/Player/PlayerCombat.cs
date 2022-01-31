using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    #region Components
    private UIManager uIManager;
    private Player player;
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
        uIManager = GameManager.GetInstance.GetUIManager;

        uIManager.ComboNumber(player.ComboAttack);
    }

    #region Attacking
    private void OnSimpleAttack()
    {
        // it works as a cooldown, it resets on StopAttack
        if (player.IsAttacking) return;

        StartCoroutine("StartAttack");
    }

    private IEnumerator StartAttack()
    {
        player.IsAttacking = true;

        yield return new WaitForSeconds(player.AttackStartTime);

        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                                player.AttackPoint.position,
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

        StartCoroutine("StopAttack");
    }

    private IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(player.AttackDuration);

        player.IsAttacking = false;
        player.ComboAttack += 1;
        uIManager.ComboNumber(player.ComboAttack);

        // resets combo timer
        player.ComboTimer = 0;
    }
    #endregion
}
