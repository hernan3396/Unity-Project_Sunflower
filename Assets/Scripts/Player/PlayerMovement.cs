using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]

public class PlayerMovement : MonoBehaviour
{
    #region Components
    private Player player;
    private Rigidbody2D rb;
    #endregion

    #region Variables
    // Mov direction
    private Vector2 direction;
    #endregion

    #region Methods
    private void Start()
    {
        player = GetComponent<Player>();
        rb = player.GetRb;
    }

    private void OnMove(InputValue inputValue)
    {
        direction = inputValue.Get<Vector2>();
    }

    private void LateUpdate()
    {
        rb.velocity = direction * player.MovementSpeed;
    }
    #endregion
}
