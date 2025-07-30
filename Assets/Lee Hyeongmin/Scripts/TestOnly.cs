using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TestOnly : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
