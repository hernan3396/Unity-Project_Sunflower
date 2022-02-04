using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    #region Components
    private UIManager uIManager;
    private Animator animator;
    private Rigidbody2D rb;
    private Seeker seeker;
    #endregion

    #region Parameters
    [Header("Parameters")]
    [SerializeField, Range(0, 9999)] private int health;
    [SerializeField, Range(0, 1000)] private float speed;
    #endregion

    #region Pathfinding
    private float nextWaypointDistance = 0.8f;
    private bool reachedEndOfPath = false;
    private int currentWaypoint = 0;
    private Path path;
    #endregion

    #region EnemyFinding
    [SerializeField] private Transform target;
    #endregion


    private void Start()
    {
        uIManager = GameManager.GetInstance.GetUIManager;

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();

        // generate path
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void FixedUpdate()
    {
        #region Pathfinding
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // movement to target
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        // checks distance to waypoint and updates it
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) currentWaypoint++;
        #endregion

        #region Animations
        if (rb.velocity != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("hMovement", force.x);
            animator.SetFloat("vMovement", force.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        #endregion
    }

    #region Damage
    public void TakeDamage(int value)
    {
        // move later to ui and use variables
        uIManager.DamageTxt(transform.position, value);

        health -= value;

        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Pathfinding
    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    #endregion
}
