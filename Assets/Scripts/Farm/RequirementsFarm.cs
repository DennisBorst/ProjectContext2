﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsFarm : Interactable
{
    //private serialized
    [SerializeField] private int increaseWater;
    [SerializeField] private int maxWaterSize; //same int as the farmstats int
    [SerializeField] private float slowWalking;

    //private
    private int currentWater;
    private float movementSpeed;
    private float decreasePercentage;
    private FarmStats farmStats;

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        farmStats = collider.gameObject.GetComponent<FarmStats>();
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }

    private void Start()
    {
        movementSpeed = woman.movementSpeed;
        slowWalking *= ((float)increaseWater / (float)maxWaterSize);
    }

    private void Update()
    {
        if (womanCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                farmStats.ChangeWaterNumber(increaseWater);
                currentWater += increaseWater;
                movementSpeed -= slowWalking;
                woman.ResetCharacter(movementSpeed);
            }
        }

        if(farmStats.currentWaterCarryAmount < currentWater)
        {
            currentWater -= increaseWater;
            movementSpeed += slowWalking;
            woman.ResetCharacter(movementSpeed);
        }
    }
}
