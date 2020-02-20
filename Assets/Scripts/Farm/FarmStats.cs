﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmStats : MonoBehaviour
{
    //public
    public bool emptyHanded = true;
    [HideInInspector] public bool carryingMais;
    [HideInInspector] public bool outOfSeeds;
    [HideInInspector] public bool outOfWater;
    [HideInInspector] public int currentWaterCarryAmount;
    public int currentSeedCarryAmount { get; set; }


    //private serialized
    [SerializeField] private int maxWaterCarryAmount;
    [SerializeField] private int maxSeedCarryAmount;
    [SerializeField] private GameObject mais;

    //private
    private Player woman;

    private void Start()
    {
        if (GetComponent<Women>())
        {
            woman = GetComponent<Player>();
        }
    }

    private void Update()
    {
        if (mais != null)
        {
            if (!emptyHanded)
            {
                carryingMais = true;
                mais.SetActive(true);
            }
            else
            {
                carryingMais = false;
                mais.SetActive(false);
            }
        }
    }

    public void ChangeWaterNumber(int changingNumber)
    {
        currentWaterCarryAmount += changingNumber;

        if (currentWaterCarryAmount > maxWaterCarryAmount)
        {
            currentWaterCarryAmount = maxWaterCarryAmount;
        }

        if (currentWaterCarryAmount <= 0)
        {
            currentWaterCarryAmount = 0;
            outOfWater = true;

            woman.SetAnimation("waterIsEmpty", true);
        }
        else
        {
            outOfWater = false;

            woman.SetAnimation("waterIsEmpty", false);
        }

        UIManagerFarm.Instance.WaterGirl(currentWaterCarryAmount, maxWaterCarryAmount);
    }

    public void ChangeSeedNumber(int changingNumber)
    {
        currentSeedCarryAmount += changingNumber;

        if (currentSeedCarryAmount > maxSeedCarryAmount)
        {
            currentSeedCarryAmount = maxSeedCarryAmount;
        }

        if (currentSeedCarryAmount <= 0)
        {
            currentSeedCarryAmount = 0;
            outOfSeeds = true;
        }
        else
        {
            outOfSeeds = false;
        }

        UIManagerFarm.Instance.SeedsBoy(currentSeedCarryAmount, maxSeedCarryAmount);
    }
}
