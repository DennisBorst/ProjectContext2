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
    public float speed;
    public StateEnum startState;

    public FSM fsm;
    public BlackBoard blackBoard = new BlackBoard();

    //private serialized
    [SerializeField] private float viewRadius;
    [SerializeField] private float viewAngle;

    private void Awake()
    {
        blackBoard.speed = speed;
        blackBoard.viewRadius = viewRadius;
        blackBoard.viewAngle = viewAngle;
        blackBoard.navMeshAgent = GetComponent<NavMeshAgent>();

        fsm = new FSM(blackBoard, startState, 
            new IdleState(StateEnum.Idle), 
            new WanderState(StateEnum.Wander), 
            new ChaseState(StateEnum.Chase), 
            new AttackState(StateEnum.Attack));
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }
    }
}
