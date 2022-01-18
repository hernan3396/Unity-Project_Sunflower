using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;
    private Animator animator;
    private UIManager uiManager;
    #endregion
    #region PlayerEnum
    public enum State
    {
        Idle,
        Walking
    }
    #endregion
    #region Movement
    [Header("Movement")]
    [SerializeField, Range(5, 20)] private int movementSpeed = 9;
    [SerializeField, Range(1, 10)] private int movSmoothness = 2;
    private Vector2 currentDirection;
    private int sprintingSpeed;
    private Vector2 direction;
    private int walkingSpeed;
    [Space]
    #endregion
    #region Shadow/Light
    private List<Transform> objectives = new List<Transform>();
    private EnemiesManager enemiesManager;
    private bool nearLight = false;
    #endregion
    #region State
    private State currentState;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        enemiesManager = GameManager.GetInstance.GetEnemiesManager;
        uiManager = GameManager.GetInstance.GetUIManager;

        sprintingSpeed = movementSpeed * 2;
        walkingSpeed = movementSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        direction = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        // smooth movement
        DOTween.To(() => currentDirection, x => currentDirection = x, direction, movSmoothness * Time.deltaTime);

        if (nearLight)
        {
            foreach (Transform objective in objectives)
            {
                // checks if player is inside a light
                Debug.DrawRay(transform.position,
                    objective.position - transform.position, Color.blue);

                RaycastHit2D ray = Physics2D.Raycast(transform.position,
                    objective.position - transform.position);

                if (ray.collider.CompareTag("LightSource"))
                {
                    enemiesManager.PlayerEnterLight();
                }
            }
        }
        uiManager.PlayerState(currentState);
    }

    #region Movement
    private void OnSprint()
    {
        // TODO: pasar a un lerp
        movementSpeed = sprintingSpeed;
    }

    private void OnStopSprint()
    {
        movementSpeed = walkingSpeed;
    }
    #endregion

    #region Shadow/Light
    // cant use generic ontrigger script cause
    // you need other's position
    // cant use overlap circle because light source's
    // range may vary
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            nearLight = true;
            objectives.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Light"))
        {
            objectives.Remove(other.transform);
            if (objectives.Count < 1)
            {
                nearLight = false;
                enemiesManager.PlayerExitLight();
            }
        }
    }
    #endregion

    #region Getter/Setter
    public Rigidbody2D GetRb
    {
        get { return rb; }
    }

    public Animator GetAnimator
    {
        get { return animator; }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
    }

    public Vector2 CurrentDirection
    {
        get { return currentDirection; }
    }

    public Vector2 Direction
    {
        get { return direction; }
    }

    public State CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }
    #endregion
}
