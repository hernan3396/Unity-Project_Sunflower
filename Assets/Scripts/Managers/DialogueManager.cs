using UnityEngine;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextAsset textAssetData;
    [SerializeField] private int dataRows;
    private DialogueList myDialogueList = new DialogueList();

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

    private void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = (data.Length / dataRows) - 1;

        myDialogueList.dialogues = new Dialogue[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            myDialogueList.dialogues[i] = new Dialogue();
            myDialogueList.dialogues[i].name = data[4 * (i + 1)];
            myDialogueList.dialogues[i].dialogueNum = Int32.Parse(data[4 * (i + 1) + 1]);
            myDialogueList.dialogues[i].dialogueEsp = data[4 * (i + 1) + 2];
            myDialogueList.dialogues[i].dialogueEng = data[4 * (i + 1) + 3];
        }
    }

    private void Start()
    {
        ReadCSV();
    }

    public void ShowText(int i)
    {
        Debug.Log(myDialogueList.dialogues[i].name);
        Debug.Log(myDialogueList.dialogues[i].dialogueEsp);
        Debug.Log(myDialogueList.dialogues[i].dialogueEng);
    }
}
