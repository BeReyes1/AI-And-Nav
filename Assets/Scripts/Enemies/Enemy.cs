using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform target;
    public NavMeshAgent Agent { get; private set; } 
    private Renderer rend;

    [Header("Variables")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float viewAngle = 60f;     

    public Transform Target => target;
    public Transform[] Waypoints => waypoints;
    public float ViewDistance => viewDistance;
    public float ViewAngle => viewAngle;
    public int CurrentWaypoint { get; private set; }
    public bool PlayerInSight { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        Vector3 dirToPlayer = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (distance <= viewDistance && angle <= viewAngle)
        {
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dirToPlayer, out RaycastHit hit, viewDistance))
            {
                if (hit.collider.GetComponent<Player>() != null)
                {
                    PlayerInSight = true;
                    return;
                }
            }
        }

        PlayerInSight = false;
    }

    public void UpdateWaypoint(int point)
    {
        CurrentWaypoint = point;
    }

    public void UpdateColor(Color color)
    {
        rend.material.color = color;
    }

}
