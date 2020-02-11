using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSeedStats : Interactable
{
    //private serialized
    [SerializeField] private int increaseStat;

    //private
    private FarmStats farmStats;

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (manCollding)
        {
            farmStats = collider.gameObject.GetComponent<FarmStats>();
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    private void Update()
    {
        UpdateManActions();
    }

    private void UpdateManActions()
    {
        if (manCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                farmStats.ChangeSeedNumber(increaseStat);
            }
        }
    }
}
