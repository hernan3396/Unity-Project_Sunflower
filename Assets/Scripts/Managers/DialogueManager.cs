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
    private int dialogueNum;
    #endregion

    #region FloatingDialogues
    [SerializeField] private FloatingDialogueHolder[] floatingDialogueHolders;
    private bool isFloating;
    #endregion

    // Esta esta solo para testing
    [SerializeField] private Language language;

    private void Start()
    {
        ReadCSV();

        uIManager = GameManager.GetInstance.GetUIManager;

        foreach (FloatingDialogueHolder holder in floatingDialogueHolders)
        {
            holder.onShowDialogue += ShowFloatingDialogue;
        }
    }

    #region ReadingFile
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
    #endregion

    #region Dialogues
    public void ShowDialogue(int dialogueNum)
    {
        // dialogueNum = Dialogue value in csv

        // initial dialogue config
        uIManager.ShowDialogue();
        this.dialogueNum = dialogueNum;
        isFloating = false;

        StartReading();
    }

    public void ShowFloatingDialogue(int dialogueNum, Vector2 position)
    {
        // dialogueNum = Dialogue value in csv

        // initial dialogue config
        uIManager.ShowFloatingDialogue(position);
        this.dialogueNum = dialogueNum;
        isFloating = true;

        StartReading();
    }

    public void StartReading()
    {
        switch (language)
        {
            case Language.Espanol:
                StartCoroutine("ReadingDialogue", myDialogueList.dialogues[dialogueNum].dialogueEsp);
                break;
            case Language.Ingles:
                StartCoroutine("ReadingDialogue", myDialogueList.dialogues[dialogueNum].dialogueEng);
                break;
        }
    }

    IEnumerator ReadingDialogue(string dialogue)
    {
        // no se uso el foreach porque necesitas saber
        // cuando termina el dialogo
        for (int i = 0; i < dialogue.Length; i++)
        {
            if (!isFloating)
            {
                uIManager.UpdateDialogue(dialogue[i]);
            }
            else
            {
                uIManager.UpdateFloatingDialogue(dialogue[i]);
            }

            if (dialogue[i] == '.')
            {
                yield return new WaitForSeconds(characterDelay * 5);

                if (i < dialogue.Length - 1)
                    uIManager.ClearDialogue(isFloating);
                // dialogue.Length - 1 es el punto final para que no se borre el texto
            }

            yield return new WaitForSeconds(characterDelay);
        }
    }

    public void StopDialogue()
    {
        StopAllCoroutines();

        if (isFloating) uIManager.HideFloatingDialogue();
        else uIManager.HideDialogue();
    }
    #endregion

    private void OnDestroy()
    {
        foreach (FloatingDialogueHolder holder in floatingDialogueHolders)
        {
            holder.onShowDialogue -= ShowFloatingDialogue;
        }
    }

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
