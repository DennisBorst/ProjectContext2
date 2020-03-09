using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackBoard
{
    public float viewRadius;
    public float viewAngle;
    public Transform[] wanderPoints;
    public List<Transform> players;
    public NavMeshAgent navMeshAgent;

    public NPCStealth npcStealth;
}
