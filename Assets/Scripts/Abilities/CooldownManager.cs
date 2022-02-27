using UnityEngine;
using System.Collections.Generic;

public class CooldownManager : MonoBehaviour
{
    private static CooldownManager instance;

    private List<Spell> spellsOnCD = new List<Spell>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void StartCooldown(Spell spell)
    {
        if (!spellsOnCD.Contains(spell))
        {
            spell.CurrentCooldown = spell.CooldownTime;
            spellsOnCD.Add(spell);
        }
    }

    private void Update()
    {
        if (spellsOnCD.Count == 0) return;

        for (int i = 0; i < spellsOnCD.Count; i++)
        {
            spellsOnCD[i].CurrentCooldown -= Time.deltaTime;

            if (spellsOnCD[i].CurrentCooldown <= 0)
            {
                spellsOnCD[i].CurrentCooldown = 0;
                spellsOnCD[i].OutOfCooldown();
                spellsOnCD.Remove(spellsOnCD[i]);
            }
        }

        // foreach da error al sacar un item de la lista si hay mas de 1
        // for no da este error
        // foreach (Spell spell in spellsOnCD)
        // {
        //     spell.CurrentCooldown -= Time.deltaTime;

        //     if (spell.CurrentCooldown <= 0)
        //     {
        //         spell.CurrentCooldown = 0;
        //         spell.OutOfCooldown();
        //         spellsOnCD.Remove(spell);
        //         // al sacar el hechizo de la lista y que quede vacio
        //         // da error al intentar hacer el siguiente foreach
        //         // por eso salimos del loop si no hay nada mas
        //         if (spellsOnCD.Count == 0) return;
        //     }
        // }
    }

    private void OnDestroy()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    public static CooldownManager GetInstance
    {
        get { return instance; }
    }
}
