using UnityEngine;
using System;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    #region Enums
    enum Column
    {
        Personaje,
        Dialogo,
        Espanol,
        Ingles
    }

    enum Language
    {
        Espanol,
        Ingles
    }
    #endregion

    #region Components
    [SerializeField] private TextAsset textAssetData;
    private UIManager uIManager;
    #endregion

    #region DialogueConfig
    [SerializeField, Range(0, 5)] private float characterDelay;
    [SerializeField] private int dataColumn; // n of rows in csv
    private DialogueList myDialogueList = new DialogueList();
    #endregion

    // Esta esta solo para testing
    [SerializeField] private Language language;

    private void Start()
    {
        ReadCSV();

        uIManager = GameManager.GetInstance.GetUIManager;
    }

    #region Dialogues
    private void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = (data.Length / dataColumn) - 1;

        myDialogueList.dialogues = new Dialogue[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myDialogueList.dialogues[i] = new Dialogue();
            myDialogueList.dialogues[i].name = data[4 * (i + 1) + (int)Column.Personaje];
            myDialogueList.dialogues[i].dialogueNum = Int32.Parse(data[4 * (i + 1) + (int)Column.Dialogo]);
            myDialogueList.dialogues[i].dialogueEsp = data[4 * (i + 1) + (int)Column.Espanol];
            myDialogueList.dialogues[i].dialogueEng = data[4 * (i + 1) + (int)Column.Ingles];
        }
    }

    public void ShowDialogue(int rowDialogue)
    {
        // rowDialogue = Dialogue value in csv
        uIManager.ShowDialogue();

        switch (language)
        {
            case Language.Espanol:
                StartCoroutine("ReadingDialogue", myDialogueList.dialogues[rowDialogue].dialogueEsp);
                break;
            case Language.Ingles:
                StartCoroutine("ReadingDialogue", myDialogueList.dialogues[rowDialogue].dialogueEng);
                break;
        }
    }

    IEnumerator ReadingDialogue(string dialogue)
    {
        foreach (char character in dialogue)
        {
            uIManager.UpdateDialogue(character);
            yield return new WaitForSeconds(characterDelay);
        }
    }

    public void StopDialogue()
    {
        StopAllCoroutines();
        uIManager.HideDialogue();
    }
    #endregion

    #region Classes
    private class Dialogue
    {
        public string name;
        public int dialogueNum;
        public string dialogueEsp;
        public string dialogueEng;
    }

    private class DialogueList
    {
        public Dialogue[] dialogues;
    }
    #endregion
}
