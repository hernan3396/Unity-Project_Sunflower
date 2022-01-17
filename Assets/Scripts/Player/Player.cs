using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;
[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;
    private Animator animator;
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
    [SerializeField] private float movementSpeed = 7f;
    private Vector2 direction;
    [Space]
    #endregion
    #region Shadow/Light
    private List<Transform> objectives = new List<Transform>();
    private EnemiesManager enemiesManager;
    private bool nearLight = false;
    #endregion
    #region State
    [Header("State")]
    // TODO: pasar esto a UIManager
    [SerializeField] private TMP_Text currentStateText;
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
    }

    private void OnMove(InputValue inputValue)
    {
        direction = inputValue.Get<Vector2>();
    }

    private void Update()
    {
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
        currentStateText.text = "State: " + currentState.ToString();
    }

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
