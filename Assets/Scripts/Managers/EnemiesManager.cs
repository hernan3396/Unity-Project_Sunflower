using UnityEngine;
using TMPro;

public class EnemiesManager : MonoBehaviour
{
    #region Components
    [SerializeField] private bool playerOnLight = false;
    private UIManager uiManager;
    #endregion

    private void Start()
    {
        uiManager = GameManager.GetInstance.GetUIManager;
    }

    #region PlayerVisibility
    public void PlayerEnterLight()
    {
        playerOnLight = true;
        uiManager.PlayerStatus("Visible");
    }

    public void PlayerExitLight()
    {
        playerOnLight = false;
        uiManager.PlayerStatus("Hidden  ");
    }
    #endregion

}
