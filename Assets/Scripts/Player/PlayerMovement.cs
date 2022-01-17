using UnityEngine;

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

    private void LateUpdate()
    {
        rb.velocity = player.Direction * player.MovementSpeed;
    }
    #endregion
}
