using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAcross : Interactable
{
    [SerializeField] private bool oneWayPath;
    [SerializeField] private bool manUse;
    [SerializeField] private bool womanUse;
    [Space]
    [SerializeField] private Vector3 movingForward;
    [SerializeField] private Vector3 movingBackwards;

    private float interactTime = 0.5f;
    private float currentInteractTimerMan;
    private float currentInteractTimerWoman;

    private bool manCrossed;
    private bool womanCrossed;


    void Start()
    {
        currentInteractTimerMan = interactTime;
        currentInteractTimerWoman = interactTime;
    }

    private void Update()
    {
        if(manCollding && manUse)
        {
            currentInteractTimerMan = Timer(currentInteractTimerMan);

            if(currentInteractTimerMan > 0)
            {
                return;
            }
            else
            {
                currentInteractTimerMan = 0;

                if (man.interact)
                {
                    currentInteractTimerMan = interactTime;

                    if (!manCrossed)
                    {
                        man.gameObject.transform.position += movingForward;
                        if (!oneWayPath)
                        {
                            manCrossed = true;
                        }
                    }
                    else
                    {
                        man.gameObject.transform.position += movingBackwards;
                        manCrossed = false;
                    }
                }
            }


        }
        else if (womanCollding && womanUse)
        {
            currentInteractTimerMan = Timer(currentInteractTimerMan);

            if (currentInteractTimerMan > 0)
            {
                return;
            }
            else
            {
                currentInteractTimerMan = 0;

                if (woman.interact)
                {
                    currentInteractTimerMan = interactTime;

                    if (!womanCrossed)
                    {
                        woman.gameObject.transform.position += movingForward;
                        if (!oneWayPath)
                        {
                            womanCrossed = true;
                        }
                    }
                    else
                    {
                        woman.gameObject.transform.position += movingBackwards;
                        womanCrossed = false;
                    }
                }
            }
        }
    }
    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }
    public override void SwitchBool(bool man)
    {
        base.SwitchBool(man);
    }
    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
