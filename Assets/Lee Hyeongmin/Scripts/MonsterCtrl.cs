using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
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
    public float viewAngle;

    private NavMeshAgent navMeshAgent;
    private bool hasTarget;
    //private bool hasSubTarget;
    private float lostTraceTime = 0;

    private float patrolDelay = 3f; // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ï¶ï¿½ 5ï¿½Ê±ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½ ï¿½Ìµï¿½
    private float patrolTimer = 4.5f;

    // ÆÛÇÃ·º½ÃÆ¼
    private bool isMovingToCarpet = false;      // ¼Ò¸® À§Ä¡·Î ÀÌµ¿ Áß ¿©ºÎ
    private Vector3 targetCarpetPos;            // ÀÌµ¿ÇÒ Ä«Æê À§Ä¡
    private float carpetArriveThreshold = 0.75f; // µµÂø ÆÇÁ¤ ÃÖ¼Ò °Å¸®

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = traceSpeed;

        hasTarget = false;
        //hasSubTarget = false;
    }

    //private void Update()
    //{
    //    if (hasTarget)
    //    {
    //        //hasSubTarget = false;
    //        isMovingToCarpet = false; // carpet ÃßÀûÀº Ãë¼Ò
    //        print("Å¸°ÙÀÖÀ½");
    //        navMeshAgent.SetDestination(target.position);

    //        float currentDistance = Vector3.Distance(transform.position, target.position);

    //        if (currentDistance > viewDistance)
    //        {
    //            lostTraceTime += Time.deltaTime;
    //        }
    //        else
    //        {
    //            lostTraceTime = 0f;
    //        }

    //        if (lostTraceTime >= 5f)
    //        {
    //            hasTarget = false;
    //            //navMeshAgent.isStopped = true; // ÀÓ½Ã¿ë, ³ªÁß¿¡ ¹Ù²ã¾ßÇØ
    //        }
    //    }
    //    else
    //    {
    //        FindTarget();
    //        if (isMovingToCarpet)
    //        {
    //            // ¼Ò¸® ³­ À§Ä¡·Î ÀÌµ¿
    //            navMeshAgent.SetDestination(targetCarpetPos);

    //            // µµÂø ÆÇÁ¤
    //            float dist = Vector3.Distance(transform.position, targetCarpetPos);
    //            if (dist <= carpetArriveThreshold)
    //            {
    //                print("Ä«Æê±îÁöÀÌµ¿²ý");
    //                isMovingToCarpet = false;
    //                //hasSubTarget = false;
    //                RandomPatrol(); // ´Ù½Ã ¼øÂû ½ÃÀÛ
    //            }
    //        }
    //        else
    //        {
    //            RandomPatrol();
    //        }
    //    }
    //}

    private void Update()
    {
        // Å¸°ÙÀÌ ÀÖ´Ù¸é ¹«Á¶°Ç Ä«Æê ÃßÀûµµ Áß´ÜÇÏ°í Å¸°Ù ÃßÀû ¿ì¼±
        if (hasTarget)
        {
<<<<<<< Updated upstream
=======
            isMovingToCarpet = false; // ¹Ýµå½Ã ¼Ò¸® ÃßÀû Áß´Ü
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                //navMeshAgent.isStopped = true; // ï¿½Ó½Ã¿ï¿½, ï¿½ï¿½ï¿½ß¿ï¿½ ï¿½Ù²ï¿½ï¿½ï¿½ï¿½
=======
>>>>>>> Stashed changes
            }
            return; // *** hasTargetÀÏ ¶§ ¾Æ·¡ ÄÚµå ½ÇÇà ¸·±â (¸Å¿ì Áß¿ä)
        }

        // Ç×»ó Å¸°Ù Å½»ö! (Ä«ÆêÀÌµç ¼øÂûÀÌµç)
        FindTarget();

        // ¼Ò¸® ÀÌµ¿
        if (isMovingToCarpet)
        {
            navMeshAgent.SetDestination(targetCarpetPos);

            float dist = Vector3.Distance(transform.position, targetCarpetPos);
            if (dist <= carpetArriveThreshold)
            {
                print("Ä«Æê±îÁöÀÌµ¿²ý");
                isMovingToCarpet = false;
                RandomPatrol();
            }
            // return ºÙ¿©µµ ¹«¹æ
            return;
        }

        // ±âº» ¼øÂû
        RandomPatrol();
    }

    //private IEnumerator RandomPatrol()
    //{
    //    print("ï¿½Ú·ï¿½Æ¾ ï¿½ß»ï¿½");
    //    Vector3 patrolTargetPosition = GetRandomPointOnNavMesh(transform.position, 10f, NavMesh.AllAreas);
    //    navMeshAgent.SetDestination(patrolTargetPosition);
    //    yield return new WaitForSeconds(5f);
    //}

    public void HeardSound(Vector3 inputCarpetPos)
    {
        // ¼Ò¸® ÀÌµ¿ »óÅÂ ÁøÀÔ
        isMovingToCarpet = true;
        //hasSubTarget = true;        // Patrol µî Á¶°Ç¹®¿¡¼­ »ç¿ë
        targetCarpetPos = inputCarpetPos;
        navMeshAgent.SetDestination(targetCarpetPos);
    }

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
        Collider[] colliders = Physics.OverlapSphere(transform.position, viewDistance, whatIsTarget);

        foreach (Collider collider in colliders)
        {
            float halfAngleInRadians = (viewAngle * 0.5f) * Mathf.Deg2Rad;

            Vector3 directionVector1 = transform.TransformDirection(Vector3.forward).normalized;
            Vector3 directionVector2 = (collider.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(directionVector1, directionVector2);

            if (dotProduct > Mathf.Cos(halfAngleInRadians))
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
    }

    //private IEnumerator UpdatePath()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.2f);

    //        if (hasTarget)
    //        {
    //            print("Å¸ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½");

    //            navMeshAgent.SetDestination(target.position);
    //        }
    //        else // Å¸ï¿½ï¿½ï¿½ï¿½ Ã£ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½Â¶ï¿½ï¿½
    //        {
    //            print("Å¸ï¿½Ù¾ï¿½ï¿½ï¿½");
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


    public Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance, int areaMask) // (ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡, ï¿½Ý°ï¿½(ï¿½Å¸ï¿½), ï¿½Ø´ï¿½Ç´ï¿½ NevMesh)
    {
        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ý°ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ø´ï¿½Ç´ï¿½ NavMesh ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½ ï¿½ï¿½È¯ï¿½ï¿½ï¿½ï¿½

        var randomPos = Random.insideUnitSphere * distance + center;    // centerï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ distanceï¿½ï¿½ ï¿½ï¿½ ï¿½È¿ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½Ä¡

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, areaMask); // (ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡, outï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½, ï¿½Ý°ï¿½(ï¿½Å¸ï¿½), ï¿½ï¿½ï¿½ï¿½ï¿½î¸¶ï¿½ï¿½Å©)  // ï¿½ï¿½ï¿½ï¿½ï¿½î¸¶ï¿½ï¿½Å©ï¿½ï¿½ ï¿½Ø´ï¿½ï¿½Ï´ï¿½ NavMesh ï¿½ß¿ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½ï¿½ï¿½ ï¿½Å¸ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ý°ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Ä¡ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½Ï³ï¿½ï¿½ï¿½ hitï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

        return hit.position;
    }

    // È®ï¿½Î¿ï¿½
    public float gizmoRadius = 10f;
    public float gizmoDistance = 10f;
    public float angle = 80f;

    public Color gizmoColor;
    private void OnDrawGizmos()
    {
        // ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½
        Gizmos.color = gizmoColor = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);

        // ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½
        // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        Vector3 start = transform.position;

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½(Transformï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½, ï¿½ï¿½ Z+ ï¿½ï¿½ï¿½ï¿½)
        float halfAngleInRadius = angle / 2 * Mathf.Deg2Rad;

        Vector3 leftDir = transform.rotation * new Vector3(-Mathf.Sin(halfAngleInRadius), 0f, Mathf.Cos(halfAngleInRadius)).normalized;
        Vector3 leftVector = start + (leftDir * gizmoDistance);
        Vector3 rightDir = transform.rotation * new Vector3(Mathf.Sin(halfAngleInRadius), 0f, Mathf.Cos(halfAngleInRadius)).normalized;
        Vector3 rightVector = start + (rightDir * gizmoDistance);

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½×¸ï¿½ï¿½ï¿½
        Gizmos.color = gizmoColor = Color.red;
        Gizmos.DrawLine(start, leftVector);
        Gizmos.DrawLine(start, rightVector);
    }
}
