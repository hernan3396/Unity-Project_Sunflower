using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    [SerializeField] private bool useMask = false;
    [SerializeField] private LayerMask layer;
    [SerializeField] private string objective;
    public UnityEvent onEnter;
    public UnityEvent onExit;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!useMask && other.gameObject.CompareTag(objective) ||
            useMask && (layer.value & (1 << other.gameObject.layer)) > 0)
        {
            onEnter?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!useMask && other.gameObject.CompareTag(objective) ||
            useMask && (layer.value & (1 << other.gameObject.layer)) > 0)
        {
            onExit?.Invoke();
        }
    }

    public void TestColl()
    {
        Debug.Log("Te chocaste wey");
    }
}
