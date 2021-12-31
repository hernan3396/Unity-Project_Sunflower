using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    #region Movement
    [SerializeField] private float movementSpeed = 5f;

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }
    #endregion

    #region Getter/Setter
    // these one are generic getter/setter
    // didnt know where to place them
    public Rigidbody2D GetRb
    {
        get { return rb; }
    }
    #endregion
}
