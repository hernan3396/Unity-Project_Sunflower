using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private bool playerOnLight = false;

    public void PlayerEnterLight()
    {
        playerOnLight = true;
    }

    public void PlayerExitLight()
    {
        playerOnLight = false;
    }
}
