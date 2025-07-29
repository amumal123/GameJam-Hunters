using UnityEngine;
using UnityEngine.AI;

public class PetMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float stopDistance = 1.5f;
    public float rotSpeed = 1;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0f;
        
        if (dir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        }
        navMeshAgent.SetDestination(target.position);
    }
}
