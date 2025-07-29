using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Trace,
        Attack
    }

    public State state = State.Idle;

    public Transform playerTr;
    public LayerMask whatIsTarget;
    public float monsterSpeed;

    private NavMeshAgent navMeshAgent;
    private bool hasTarget;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = monsterSpeed;

        hasTarget = false;
    }

    private void Update()
    {
        //navMeshAgent.SetDestination(playerTr.position);

        StartCoroutine(UpdatePath());
    }

    private IEnumerator UpdatePath()
    {
        yield return new WaitForSeconds(0.2f);
        
        if (hasTarget)
        {
            
        }
        else // 타겟을 찾지 못한 상태라면
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, whatIsTarget);
            foreach (Collider collider in colliders)
            {
                print(collider.name);
            }
        }
    }




    // 확인용
    public float radius = 1f;
    public Color gizmoColor = Color.green;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
