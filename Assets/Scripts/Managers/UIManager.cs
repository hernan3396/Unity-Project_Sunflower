using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    #region Components
    [Header("Player status")]
    [SerializeField] private TMP_Text playerState;
    [SerializeField] private TMP_Text playerStatus;
    [SerializeField] private TMP_Text comboNumber;
    [Space]
    [Header("Damage")]
    [SerializeField] private GameObject damageTxt;
    [SerializeField] private float damageTxtDur;
    [Space]
    [Header("Dialogues")]
    [SerializeField] private TMP_Text dialogues;
    #endregion

    private void Start()
    {
        ComboNumber(0);
    }

    #region PlayerStatus
    public void PlayerState(Player.State currentState)
    {
        playerState.text = "State: " + currentState.ToString();
    }

    public void PlayerStatus(string value)
    {
        playerStatus.text = "Status: " + value;
    }

    public void ComboNumber(int value)
    {
        comboNumber.text = "Combo N: " + value;
    }
    #endregion

    #region Damage
    public void DamageTxt(Vector2 position, int value)
    {
        damageTxt.gameObject.SetActive(true);
        damageTxt.GetComponent<RectTransform>().position = position;
        damageTxt.GetComponentInChildren<TMP_Text>().text = value.ToString();
        damageTxt.GetComponentInChildren<Animator>().Play("FloatingDamage");

        StartCoroutine("DeactivateDamageTxt");
    }

    private IEnumerator DeactivateDamageTxt()
    {
        yield return new WaitForSeconds(damageTxtDur);
        damageTxt.gameObject.SetActive(false);
    }
    #endregion

    #region Dialogues
    public void ShowDialogue()
    {
        dialogues.gameObject.SetActive(true);
        dialogues.text = string.Empty;
    }

    public void HideDialogue()
    {
        dialogues.gameObject.SetActive(false);
    }

    public void UpdateDialogue(char value)
    {
        dialogues.text += value;
    }
    #endregion
}
