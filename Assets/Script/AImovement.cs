using UnityEngine;
using UnityEngine.AI;

public class AImovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3.5f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        agent.speed = moveSpeed;
    }
}


