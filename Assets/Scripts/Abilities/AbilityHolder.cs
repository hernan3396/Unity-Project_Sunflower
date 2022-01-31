using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    // esto ajustarlo
    [SerializeField] private Ability[] ability;
    private bool attackOrder = true;

    enum AbilityName
    {
        SimpleAttack,
        HeavyAttack
    }

    private void OnSimpleAttack()
    {
        if (attackOrder)
        {
            ability[(int)AbilityName.SimpleAttack].Activate();
        }
        else
        {
            ability[(int)AbilityName.HeavyAttack].Activate();
        }

        attackOrder = !attackOrder;
    }
}
