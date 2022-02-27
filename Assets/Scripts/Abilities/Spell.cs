using UnityEngine;
[CreateAssetMenu(fileName = "NewSpell", menuName = "Spell")]
public class Spell : Ability
{
    public override void Activate(Transform position)
    {
        if (!IsSpellReady())
        {
            Debug.Log("Hechizo no listo " + abilityName);
            return;
        }
        else
        {
            Debug.Log("Casteando hechizo " + abilityName);
        }

        base.Activate(position);
        PutOnCooldown();
    }
    protected void PutOnCooldown()
    {
        CooldownManager.GetInstance.StartCooldown(this);
    }

    public void OutOfCooldown()
    {
        Debug.Log(abilityName + " Salio de cooldown");
    }

    private bool IsSpellReady()
    {
        if (currentCooldown <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
