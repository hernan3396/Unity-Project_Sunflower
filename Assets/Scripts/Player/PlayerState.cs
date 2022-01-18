using UnityEngine;

public class PlayerState : MonoBehaviour
{
    #region Components
    private Player player;
    private Animator animator;
    #endregion

    #region Methods
    private void Start()
    {
        player = GetComponent<Player>();
        animator = player.GetAnimator;
    }

    private void Update()
    {
        ManageState();
        ManageAnimations();
    }
    #endregion

    #region ManageStates
    // logic for managing states and animations
    private void ManageState()
    {
        player.CurrentState = player.CurrentDirection != Vector2.zero
                            ? Player.State.Walking : Player.State.Idle;
    }

    private void ManageAnimations()
    {
        switch (player.CurrentState)
        {
            case Player.State.Walking:
                animator.SetBool("isMoving", true);
                animator.SetFloat("hMovement", player.CurrentDirection.x);
                animator.SetFloat("vMovement", player.CurrentDirection.y);
                break;
            default:
                animator.SetBool("isMoving", false);
                break;
        }
    }
    #endregion
}
