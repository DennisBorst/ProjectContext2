using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOn : Interactable
{

    [SerializeField] private float lerpSpeed;
    [SerializeField] private float distanceToTeleport;
    [SerializeField] private Transform beginPoint;
    [SerializeField] private Transform endPoint;

    private float interactTime = 0.5f;
    private float currentInteractTimerMan;
    private float currentInteractTimerWoman;

    private bool manLerping;
    private bool womanLerping;

    void Start()
    {
        currentInteractTimerMan = interactTime;
        currentInteractTimerWoman = interactTime;
    }

    private void Update()
    {
        Input();

        if (manLerping)
        {
            man.gameObject.transform.position = Vector3.Lerp(man.gameObject.transform.position, beginPoint.position, lerpSpeed);

            float distance = Vector3.Distance(man.gameObject.transform.position, beginPoint.position);
            if (distance <= distanceToTeleport)
            { 
                manLerping = false;
                man.gameObject.transform.position = endPoint.position;
            }
        }
        if (womanLerping)
        {
            woman.gameObject.transform.position = Vector3.Lerp(woman.gameObject.transform.position, beginPoint.position, lerpSpeed);

            float distance = Vector3.Distance(woman.gameObject.transform.position, beginPoint.position);
            if (distance <= distanceToTeleport)
            {
                womanLerping = false;
                woman.gameObject.transform.position = endPoint.position;
            }
        }
    }

    private void Input()
    {
        if (manCollding)
        {
            currentInteractTimerMan = Timer(currentInteractTimerMan);

            if (currentInteractTimerMan > 0)
            {
                return;
            }
            else
            {
                currentInteractTimerMan = 0;

                if (man.interact)
                {
                    currentInteractTimerMan = interactTime;
                    manLerping = true;
                }
            }

        }
        else if (womanCollding)
        {
            currentInteractTimerWoman = Timer(currentInteractTimerWoman);

            if (currentInteractTimerWoman > 0)
            {
                return;
            }
            else
            {
                currentInteractTimerWoman = 0;

                if (woman.interact)
                {
                    currentInteractTimerWoman = interactTime;
                    womanLerping = true;
                }
            }
        }
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
