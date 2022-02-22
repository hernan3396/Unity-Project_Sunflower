using UnityEngine;

public class FloatingDialogueHolder : MonoBehaviour
{
    [SerializeField] protected int dialogueNum;
    public delegate void OnShowDialogue(int dialogueNum, Vector2 positon);
    public event OnShowDialogue onShowDialogue;

    public void ShowFloatingText()
    {
        if (onShowDialogue != null) onShowDialogue(dialogueNum, transform.position);
    }
}
