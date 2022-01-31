using UnityEngine;

public class Ability : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float activationTime;
    [SerializeField] protected float activeTime;

    public virtual void Activate() { }
}
