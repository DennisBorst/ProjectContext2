using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePointPlayers : MonoBehaviour
{
    [SerializeField] private List<Transform> players;

    private void LateUpdate()
    {
        Vector3 centerPoint = GetCenterPoint();

        transform.position = centerPoint;
    }
    private Vector3 GetCenterPoint()
    {
        if(players.Count == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds.center;
    }
}
