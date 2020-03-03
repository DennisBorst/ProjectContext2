using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    [SerializeField] private Transform manTeleportPoint;
    [SerializeField] private Transform womanTeleportPoint;

    private void Update()
    {
        if(womanCollding && manCollding)
        {
            man.gameObject.transform.position = manTeleportPoint.position;
            woman.gameObject.transform.position = womanTeleportPoint.position;
            this.gameObject.SetActive(false);
        }
    }
}
