using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddlePointPlayers : MonoBehaviour
{
    [SerializeField] private float walkingDistance;
    [SerializeField] private List<Player> players;

    private float distancePlayer;
    private Vector3 centerPoint;
    //private List<Vector3> previousPosition = new List<Vector3>();

    private void Start()
    {
        for (int i = 0; i < players.Count; i++)
        {
            //previousPosition.Add(players[i].transform.position);
        }
    }
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
            Debug.Log("Distance between players is to big");

            CheckPlayerPosition();
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].ResetCharacter(players[i].movementSpeed);
            }
        }
    }

    private void CheckPlayerPosition()
    {
        if(players[0].inputX > 0.5f && -0.5 > players[1].inputX || players[1].inputX > 0.5f && -0.5 > players[0].inputX ||
           players[0].inputZ > 0.5f && -0.5 > players[1].inputZ || players[1].inputZ > 0.5f && -0.5 > players[0].inputZ)
        {
            players[0].ResetCharacter(0);
            players[1].ResetCharacter(0);

            players[0].transform.position = Vector3.MoveTowards(players[0].transform.position, centerPoint, 0.1f);
            players[1].transform.position = Vector3.MoveTowards(players[1].transform.position, centerPoint, 0.1f);
        }
        else if(players[0].inputX > 0.5f || players[0].inputX < -0.5f || players[0].inputZ > 0.5f || players[0].inputZ < -0.5f)
        {
            players[0].ResetCharacter(0);
            players[0].transform.position = Vector3.MoveTowards(players[0].transform.position, centerPoint, 0.1f);
        }
        else if(players[1].inputX > 0.5f || players[1].inputX < -0.5f || players[1].inputZ > 0.5f || players[1].inputZ < -0.5f)
        {
            players[1].ResetCharacter(0);
            players[1].transform.position = Vector3.MoveTowards(players[1].transform.position, centerPoint, 0.1f);
        }
    }
    private void LateUpdate()
    {
        centerPoint = GetCenterPoint();

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
