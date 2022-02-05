using UnityEngine;

public class Ability : ScriptableObject
{
    #region AbilityParameters
    [Header("Ability Parameters")]
    [SerializeField] protected LayerMask layer;
    [SerializeField] protected string abilityName;
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float activeTime;
    [SerializeField] protected float castTime;
    [SerializeField] protected int damage;
    [SerializeField] protected float radius;
    [Space]
    #endregion

    #region ComboIntento
    [Header("Combo Intento")]
    [SerializeField] protected bool isCombo;
    [SerializeField] protected int comboLength;
    protected bool finisherReady;
    protected int combo = 0;
    #endregion

    private void Start()
    {
        // reset combo values
        // it can be != 0 on start
        combo = 0;
        finisherReady = false;
    }

    public virtual void Activate(Transform position)
    {
        // activar la habilidad
    }

    #region ComboIntento
    public void AddCombo()
    {
        // es medio al pedo hacerlos para una linea sola
        // pero esta por si hay que agregar mas logica luego
        combo += 1;
        if (isCombo) finisherReady = combo >= comboLength ? true : false;
    }

    public void ResetCombo()
    {
        // es medio al pedo hacerlos para una linea sola
        // pero esta por si hay que agregar mas logica luego
        combo = 0;
        finisherReady = false;
    }
    #endregion

    #region Getter/Setter
    public string AbilityName
    {
        get { return abilityName; }
    }

    public float CooldownTime
    {
        get { return cooldownTime; }
    }

    public float CastTime
    {
        get { return castTime; }
    }

    public float ActiveTime
    {
        get { return activeTime; }
    }

    public int Damage
    {
        get { return damage; }
    }

    public float Radius
    {
        get { return radius; }
    }

    public int ComboLength
    {
        get { return comboLength; }
    }

    public int Combo
    {
        get { return combo; }
    }

    public bool IsCombo
    {
        get { return isCombo; }
    }

    public bool FinisherReady
    {
        get { return finisherReady; }
    }
    #endregion
}
