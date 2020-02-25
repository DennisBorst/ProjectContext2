using UnityEngine;

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
    public FSM fsm;

    private void Awake()
    {
        fsm = new FSM(StateEnum.Idle, 
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
