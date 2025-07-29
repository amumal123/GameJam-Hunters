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

    public Transform target;
    public LayerMask whatIsTarget;
    public float traceSpeed;
    public float patrolSpeed;
    public float viewDistance;

    private NavMeshAgent navMeshAgent;
    private bool hasTarget;
    private float lostTraceTime = 0;

    private float patrolDelay = 5f; // 순찰중일때 5초까지만 그 위치로 이동
    private float patrolTimer = 0f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = traceSpeed;

        hasTarget = false;
    }

    private void Update()
    {
        if (hasTarget)
        {
            print("타겟있음");
            navMeshAgent.SetDestination(target.position);

            float currentDistance = Vector3.Distance(transform.position, target.position);

            if (currentDistance > viewDistance)
            {
                lostTraceTime += Time.deltaTime;
            }
            else
            {
                lostTraceTime = 0f;
            }

            if (lostTraceTime >= 5f)
            {
                hasTarget = false;
                navMeshAgent.isStopped = true; // 임시용, 나중에 바꿔야해
            }
        }
        else
        {
            //StartCoroutine(RandomPatrol());
            RandomPatrol();
            FindTarget();
        }
    }

    //private IEnumerator RandomPatrol()
    //{
    //    print("코루틴 발생");
    //    Vector3 patrolTargetPosition = GetRandomPointOnNavMesh(transform.position, 10f, NavMesh.AllAreas);
    //    navMeshAgent.SetDestination(patrolTargetPosition);
    //    yield return new WaitForSeconds(5f);
    //}

    private void RandomPatrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolDelay)
        {
            navMeshAgent.SetDestination(GetRandomPointOnNavMesh(transform.position, 10, NavMesh.AllAreas));
            patrolTimer = 0f;
        }
    }

    private void FindTarget()
    {
        print("타겟없음");
        Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance, whatIsTarget);
        foreach (Collider collider in colliders)
        {
            Vector3 direction = (collider.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance))
            {
                var hitLayer = hit.collider.gameObject.layer;
                if (((1 << hitLayer) & whatIsTarget) != 0)
                {
                    hasTarget = true;
                }
            }
        }
    }

    //private IEnumerator UpdatePath()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        if (hasTarget)
    //        {
    //            print("타겟을 가지고 있음");

    //            navMeshAgent.SetDestination(target.position);
    //        }
    //        else // 타겟을 찾지 못한 상태라면
    //        {
    //            print("타겟없음");
    //            Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance, whatIsTarget);
    //            foreach (Collider collider in colliders)
    //            {
    //                Vector3 direction = (collider.transform.position - transform.position).normalized;
    //                float distance = Vector3.Distance(transform.position, collider.transform.position);
    //                if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance))
    //                {
    //                    var hitLayer = hit.collider.gameObject.layer;
    //                    if (((1 << hitLayer) & whatIsTarget) != 0)
    //                    {
    //                        hasTarget = true;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}


    public Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask) // (현재 위치, 반경(거리), 해당되는 NevMesh)
    {
        // 기준점과 반경을 기준으로 해당되는 NavMesh 위에 랜덤한 위치를 반환해줌

        var randomPos = Random.insideUnitSphere * distance + center;    // center를 기준으로 반지름이 distance인 구 안에서 어느 한 위치

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask); // (기준 위치, out결과정보, 반경(거리), 에리어마스크)  // 에리어마스크에 해당하는 NavMesh 중에 기준 위치에서 거리 까지의 반경 내에서 기준 위치에 가장 가까운 점 하나를 hit에 담음

        return hit.position;
    }

    // 확인용
    public float radius = 10f;
    public Color gizmoColor = Color.green;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
