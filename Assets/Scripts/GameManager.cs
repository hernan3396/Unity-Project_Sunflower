using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Components
    private EnemiesManager enemiesManager;
    private UIManager uiManager;
    private RoomTemplates roomTemplates;
    #endregion
    private static GameManager instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        // components down here
        enemiesManager = GetComponent<EnemiesManager>();
        roomTemplates = GetComponent<RoomTemplates>();
        uiManager = GetComponent<UIManager>();
    }

    private void OnDestroy()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    #region Getters/Setters
    public static GameManager GetInstance
    {
        get { return instance; }
    }

    public EnemiesManager GetEnemiesManager
    {
        get { return enemiesManager; }
    }

    public RoomTemplates GetRoomTemplates
    {
        get { return roomTemplates; }
    }

    public UIManager GetUIManager
    {
        get { return uiManager; }
    }
    #endregion
}
