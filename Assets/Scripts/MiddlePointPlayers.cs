using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePointPlayers : MonoBehaviour
{
    [SerializeField] private float walkingDistance;
    [SerializeField] private List<Player> players;

    private float distancePlayer;

    private void Update()
    {
        distancePlayer = Vector3.Distance(players[0].transform.position, players[1].transform.position);

        if(distancePlayer < 0)
        {
            distancePlayer *= -1;
        }

        if(distancePlayer >= walkingDistance)
        {
            //InvisibleWall
        }
    }
    private void LateUpdate()
    {
        Vector3 centerPoint = GetCenterPoint();

        transform.position = centerPoint;
    }
    private Vector3 GetCenterPoint()
    {
        if(players.Count == 1)
        {
            return players[0].transform.position;
        }

        var bounds = new Bounds(players[0].transform.position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].transform.position);
        }

        return bounds.center;
    }
}
