using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    #region UI
    // move later
    [SerializeField] private GameObject DamageTxt;
    #endregion

    #region Parameters
    [SerializeField] private int health;
    #endregion

    public void TakeDamage(int value)
    {
        // move later to ui and use variables
        DamageTxt.gameObject.SetActive(true);
        DamageTxt.GetComponent<RectTransform>().position = transform.position;
        DamageTxt.GetComponentInChildren<TMP_Text>().text = value.ToString();
        DamageTxt.GetComponentInChildren<Animator>().Play("FloatingDamage");
        Invoke("DeactivateText", 0.9f);

        health -= value;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        this.gameObject.SetActive(false);
    }

    private void DeactivateText()
    {
        DamageTxt.gameObject.SetActive(false);
    }
}
