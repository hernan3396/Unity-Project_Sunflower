using UnityEngine;
using UnityEngine.Events;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private bool useMask = false;
    [SerializeField] private LayerMask layer;
    [SerializeField] private string objective;
    public UnityEvent onEnter;
    public UnityEvent onExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!useMask && other.CompareTag(objective) ||
          useMask && (layer.value & (1 << other.gameObject.layer)) > 0)
        {
            onEnter?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!useMask && other.CompareTag(objective) ||
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
