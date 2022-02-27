using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    #region Components
    private UIManager uIManager;
    private Player player;
    #endregion
    Coroutine stopComboCor;

    #region Abilities
    enum AbilityName
    {
        SimpleAttack,
        HeavyAttack
    }

    // spells part is simple and incomplete
    // dont want to finish it atm
    enum Spells
    {
        SimpleSpell,
        OtherSpell
    }
    [SerializeField] private Spell[] spells;

    [SerializeField] private Ability[] abilities;
    private int currentAbility;
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
        uIManager = GameManager.GetInstance.GetUIManager;

        foreach (Spell spell in spells)
        {
            spell.CurrentCooldown = 0;
        }
    }

    #region Spell
    // lazy implementation, didnt want to finish it
    // but its a solid base for a good system
    private void OnSpell()
    {
        // this is a simple method just for testing
        // change later but for this "project" is not necessary
        spells[(int)Spells.SimpleSpell].Activate(player.AttackPoint);
    }

    private void OnOtherSpell()
    {
        // this is a simple method just for testing
        // change later but for this "project" is not necessary
        spells[(int)Spells.OtherSpell].Activate(player.AttackPoint);
    }
    #endregion


    private void OnSimpleAttack()
    {
        if (player.CurrentState == Player.State.Damaged) return;

        // it works as a cooldown, it resets on StopAttack
        if (player.IsAttacking) return;

        if (abilities[currentAbility].FinisherReady)
        {
            ResetCombo();

            // uses a "Finisher"
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
        // Stops combo s  fall off timer
        if (stopComboCor != null) StopCoroutine(stopComboCor);

        yield return new WaitForSeconds(abilities[currentAbility].CastTime);
        abilities[currentAbility].Activate(player.AttackPoint);

        if (abilities[currentAbility].IsCombo)
        {
            AddToCombo();
            stopComboCor = StartCoroutine(StopCombo());
        }

        StartCoroutine("StopAttack");
    }

    private IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(abilities[currentAbility].CooldownTime);

        player.IsAttacking = false;
    }
    #endregion

    #region ComboLogic
    private IEnumerator StopCombo()
    {
        // adds fall off to combos
        yield return new WaitForSeconds(player.ComboFallOff);
        ResetCombo();
    }

    private void AddToCombo()
    {
        abilities[currentAbility].AddCombo();
        uIManager.ComboNumber(abilities[currentAbility].Combo);
    }

    private void ResetCombo()
    {
        abilities[currentAbility].ResetCombo();
        uIManager.ComboNumber(abilities[currentAbility].Combo);
    }
    #endregion

    // TODO: Implementar Sistema de cooldown para las habilidades
    // para hacer esto cada habilidad tiene que tener un monobehaviour
    // en vez de ser un scriptableobject
    // asi que si lo quiero hacer hay que hacer unos cuantos cambios
    // de momento da igual
}
