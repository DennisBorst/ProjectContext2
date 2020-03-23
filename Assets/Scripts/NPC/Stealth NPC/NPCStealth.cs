﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public enum StateEnum
{
    Idle,
    Wander,
    Chase,
    Attack,
    Talk
}

public class NPCStealth : Interactable
{
    //public
    public float chaseSpeed;
    public float walkSpeed;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public bool idleNPC;
    public bool canTalk;
    public Transform[] wanderPoints;
    public StateEnum startState;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public List<Transform> visibleTargets = new List<Transform>();
    public string dialogueTextMan;
    public string dialogueTextWoman;
    public TextMeshProUGUI dialogueUI;
    public GameObject npcObject;
    public GameObject angryItem;
    public GameObject dialogueCanvas;
    public FSM fsm;
    public BlackBoard blackBoard = new BlackBoard();

    //public hide in inspector
    public bool following;
    [HideInInspector] public Player talkingPlayer;
    [HideInInspector] public string textPlayer;

    //private
    private bool playerColliding;
    private Animator anim;
    private List<Player> player = new List<Player>();

    private void Awake()
    {
        blackBoard.viewRadius = viewRadius;
        blackBoard.viewAngle = viewAngle;
        blackBoard.wanderPoints = wanderPoints;
        blackBoard.players = visibleTargets;
        blackBoard.navMeshAgent = GetComponent<NavMeshAgent>();
        blackBoard.npcStealth = this;
        angryItem.SetActive(false);

        anim = GetComponentInChildren<Animator>();

        fsm = new FSM(blackBoard, startState, 
            new IdleState(StateEnum.Idle), 
            new WanderState(StateEnum.Wander), 
            new ChaseState(StateEnum.Chase), 
            new TalkState(StateEnum.Talk));

        if (!following)
        {
            StartCoroutine(FindTargetsDelay(0.2f));
        }
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }

        if (!canTalk)
        {
            return;
        }
        
        if(player != null && !following && talkingPlayer == null)
        {
            for (int i = 0; i < player.Count; i++)
            {
                if (player[i].interact)
                {
                    talkingPlayer = player[i];
                    if(player[i].GetComponent<Man>())
                    {
                        textPlayer = dialogueTextMan;
                    }
                    if(player[i].GetComponent<Woman>())
                    {
                        textPlayer = dialogueTextWoman;
                    }
                    fsm.SwitchState(StateEnum.Talk);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (collider.GetComponent<Woman>())
            {
                player.Add(collider.gameObject.GetComponent<Woman>());
            }

            if (collider.GetComponent<Man>())
            {
                player.Add(collider.gameObject.GetComponent<Man>());
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.GetComponent<Woman>())
            {
                player.Remove(collider.gameObject.GetComponent<Woman>());
            }

            if (collider.gameObject.GetComponent<Man>())
            {
                player.Remove(collider.gameObject.GetComponent<Man>());
            }
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
        if (following)
        {
            return;
        }

        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            GrabCheck grabFoodCheck = target.GetComponent<GrabCheck>();
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (grabFoodCheck.grabbingFood)
                    {
                        visibleTargets.Add(target);
                        following = true;
                        fsm.SwitchState(StateEnum.Chase);
                    }
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