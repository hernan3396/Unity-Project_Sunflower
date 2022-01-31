using UnityEngine;

[CreateAssetMenu]
public class SimpleAttack : Ability
{
    public override void Activate()
    {
        Debug.Log("Se activo el: " + abilityName);
    }
}
