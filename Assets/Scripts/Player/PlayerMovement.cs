using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]

public class PlayerMovement : MonoBehaviour
{
    #region Components
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    #endregion

    #region Variables
    // Mov direction
    private Vector2 direction;
    #endregion

    #region Methods
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        player = GetComponent<Player>();
        rb = player.GetRb;
    }

    private void OnMove(InputValue inputValue)
    {
        direction = inputValue.Get<Vector2>();
        animator.SetFloat("hMovement", direction.x);
        animator.SetFloat("vMovement", direction.y);
    }

    private void Update()
    {
        animator.SetBool("isMoving", direction != Vector2.zero ? true : false);
    }

    private void LateUpdate()
    {
        rb.velocity = direction * player.MovementSpeed;
    }
    #endregion
}
