using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hyena : MonoBehaviour
{
    //public
    public float sneakSpeed;
    public float attackSpeed;
    public float distanceFromPlayer;
    public float attackDistance;
    public Player[] players;

    public StateEnum startState;
    public FSM fsm;
    public BlackBoard blackBoard = new BlackBoard();

    //private
    private Animator anim;


    void Start()
    {
        blackBoard.playerScripts = players;
        blackBoard.hyena = this;
        blackBoard.navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        fsm = new FSM(blackBoard, startState,
            new IdleHyena(StateEnum.Idle),
            new WalkHyena(StateEnum.Wander),
            new AttackHyena(StateEnum.Attack));
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }
    }

    public void SetAnimation(string state, bool val)
    {
        if (state != null && val != null)
        {
            anim.SetBool(state, val);
        }
    }
    public IEnumerator SetanimationBoolFalse(string val, float duration)
    {
        yield return new WaitForSeconds(duration);
        anim.SetBool(val, false);
    }
}
