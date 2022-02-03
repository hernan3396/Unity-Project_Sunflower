using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    #region Components
    private UIManager uIManager;
    private Player player;
    #endregion

    #region Abilities
    enum AbilityName
    {
        SimpleAttack,
        HeavyAttack
    }

    [SerializeField] private Ability[] abilities;
    private int currentAbility;
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
        uIManager = GameManager.GetInstance.GetUIManager;
    }

    private void OnSimpleAttack()
    {
        // it works as a cooldown, it resets on StopAttack
        if (player.IsAttacking) return;

        if (abilities[currentAbility].Finisher())
        {
            abilities[currentAbility].ResetCombo();
            currentAbility = (int)AbilityName.HeavyAttack;
        }
        else
        {
            currentAbility = (int)AbilityName.SimpleAttack;
        }

        StartCoroutine("StartAttack");
    }

    #region AttackLogic
    // attack is selected on previous step (for example OnSimpleAttack())
    private IEnumerator StartAttack()
    {
        player.IsAttacking = true;

        yield return new WaitForSeconds(abilities[currentAbility].CastTime);

        abilities[currentAbility].Activate(player.AttackPoint.position);

        if (abilities[currentAbility].IsCombo)
        {
            abilities[currentAbility].AddCombo();
        }

        StartCoroutine("StopAttack");
    }

    private IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(abilities[currentAbility].CooldownTime);

        player.IsAttacking = false;
    }
    #endregion

    // TODO: Implementar Sistema de cooldown para las habilidades

}
