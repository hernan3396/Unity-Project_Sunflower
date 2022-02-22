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
    [SerializeField] private GameObject dialogueBackground;
    [SerializeField] private TMP_Text dialogue;
    [Space]
    [Header("Floating Dialogues")]
    [SerializeField] private GameObject floatingDialogueBackground;
    [SerializeField] private TMP_Text floatingDialogue;
    [SerializeField] private Vector2 floatingDialogueOff;
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
        dialogueBackground.SetActive(true);
        dialogue.text = string.Empty;
    }

    public void HideDialogue()
    {
        dialogueBackground.SetActive(false);
        dialogue.text = string.Empty;
    }

    public void UpdateDialogue(char value)
    {
        dialogue.text += value;
    }
    #endregion

    #region FloatingDialogues
    public void ShowFloatingDialogue(Vector2 objective)
    {
        floatingDialogueBackground.transform.position = objective + floatingDialogueOff;
        floatingDialogueBackground.SetActive(true);
        floatingDialogue.text = string.Empty;
    }

    public void HideFloatingDialogue()
    {
        floatingDialogueBackground.SetActive(false);
        floatingDialogue.text = string.Empty;
    }

    public void UpdateFloatingDialogue(char value)
    {
        floatingDialogue.text += value;
    }
    #endregion
}
