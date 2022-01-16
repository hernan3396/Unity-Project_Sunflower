using UnityEngine;
using TMPro;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private bool playerOnLight = false;
    [SerializeField] private TMP_Text playerStatus;

    public void PlayerEnterLight()
    {
        playerOnLight = true;
        playerStatus.text = "Status: Visible";
    }

    public void PlayerExitLight()
    {
        playerOnLight = false;
        playerStatus.text = "Status: Hidden";
    }
}
