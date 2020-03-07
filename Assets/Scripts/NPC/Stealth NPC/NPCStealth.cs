using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum
{
    Idle,
    Wander,
    Chase,
    Attack,
    Talk
}

public class NPCStealth : MonoBehaviour
{
    //public
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public Transform[] wanderPoints;
    public StateEnum startState;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();

    public FSM fsm;
    public BlackBoard blackBoard = new BlackBoard();

    //private serialized

    //private
    private Animator anim;

    private void Awake()
    {
        blackBoard.viewRadius = viewRadius;
        blackBoard.viewAngle = viewAngle;
        blackBoard.wanderPoints = wanderPoints;
        blackBoard.navMeshAgent = GetComponent<NavMeshAgent>();
        blackBoard.npcStealth = this;

        anim = GetComponentInChildren<Animator>();

        fsm = new FSM(blackBoard, startState, 
            new IdleState(StateEnum.Idle), 
            new WanderState(StateEnum.Wander), 
            new ChaseState(StateEnum.Chase), 
            new AttackState(StateEnum.Attack));

        StartCoroutine(FindTargetsDelay(0.2f));
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }
    }

    IEnumerator FindTargetsDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    private void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewAngle, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void SetAnimation(string state, bool val)
    {
        if (state != null && val != null)
        {
            anim.SetBool(state, val);
        }
    }
    IEnumerator SetanimationBoolFalse(string val, float duration)
    {
        Debug.Log("Made it here");
        yield return new WaitForSeconds(duration);
        anim.SetBool(val, false);
    }
}
