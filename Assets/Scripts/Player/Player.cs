using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;
    #endregion
    #region Movement
    [SerializeField] private float movementSpeed = 5f;
    #endregion
    #region Hidden/OnLight
    // for detecting if player is on a shadow or on a light 
    private bool isOnLight;
    #endregion

    // TODO: hacer la logica de detectar si estas en la luz o no aqui

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    #region Getter/Setter
    public Rigidbody2D GetRb
    {
        get { return rb; }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }
    #endregion
}
