using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    #region Components
    private UIManager uIManager;
    #endregion

    #region Parameters
    [SerializeField] private int health;
    #endregion

    private void Start()
    {
        uIManager = GameManager.GetInstance.GetUIManager;
    }

    public void TakeDamage(int value)
    {
        // move later to ui and use variables
        uIManager.DamageTxt(transform.position, value);

        health -= value;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        this.gameObject.SetActive(false);
    }
}
