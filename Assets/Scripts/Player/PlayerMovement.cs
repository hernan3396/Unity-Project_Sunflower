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
        if (player.CurrentState == Player.State.Damaged) return;

        if (player.CurrentState == Player.State.Attacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = player.CurrentDirection * player.MovementSpeed;
    }
    #endregion
}
