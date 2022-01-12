using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Components
    [SerializeField] Camera cam;
    #endregion
    private static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
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
    #endregion
}
