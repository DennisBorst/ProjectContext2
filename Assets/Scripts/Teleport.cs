using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    [SerializeField] private Transform manTeleportPoint;
    [SerializeField] private Transform womanTeleportPoint;

    private bool activated = false;
    private float currentTime;
    private float maxTime = 1f;

    private void Start()
    {
        currentTime = maxTime;
    }

    private void Update()
    {
        if(womanCollding && manCollding)
        {
            activated = true;
        }

        if (activated)
        {
            currentTime = Timer(currentTime);

            if(currentTime <= 0)
            {
                man.gameObject.transform.position = manTeleportPoint.position;
                woman.gameObject.transform.position = womanTeleportPoint.position;
                this.gameObject.SetActive(false);
            }
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
