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

    private float patrolDelay = 5f; // �������϶� 5�ʱ����� �� ��ġ�� �̵�
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
            print("Ÿ������");
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
                navMeshAgent.isStopped = true; // �ӽÿ�, ���߿� �ٲ����
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
    //    print("�ڷ�ƾ �߻�");
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
        print("Ÿ�پ���");
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
    //            print("Ÿ���� ������ ����");

    //            navMeshAgent.SetDestination(target.position);
    //        }
    //        else // Ÿ���� ã�� ���� ���¶��
    //        {
    //            print("Ÿ�پ���");
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


    public Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask) // (���� ��ġ, �ݰ�(�Ÿ�), �ش�Ǵ� NevMesh)
    {
        // �������� �ݰ��� �������� �ش�Ǵ� NavMesh ���� ������ ��ġ�� ��ȯ����

        var randomPos = Random.insideUnitSphere * distance + center;    // center�� �������� �������� distance�� �� �ȿ��� ��� �� ��ġ

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask); // (���� ��ġ, out�������, �ݰ�(�Ÿ�), �������ũ)  // �������ũ�� �ش��ϴ� NavMesh �߿� ���� ��ġ���� �Ÿ� ������ �ݰ� ������ ���� ��ġ�� ���� ����� �� �ϳ��� hit�� ����

        return hit.position;
    }

    // Ȯ�ο�
    public float radius = 10f;
    public Color gizmoColor = Color.green;
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
