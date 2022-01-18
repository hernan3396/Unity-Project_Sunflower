using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Components
    [SerializeField] private TMP_Text playerState;
    [SerializeField] private TMP_Text playerStatus;
    #endregion

    #region Methods
    public void PlayerState(Player.State currentState)
    {
        playerState.text = "State: " + currentState.ToString();
    }

    public void PlayerStatus(string value)
    {
        playerStatus.text = "Status: " + value;
    }
    #endregion
}
