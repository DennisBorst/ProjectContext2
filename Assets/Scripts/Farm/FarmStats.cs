using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmStats : MonoBehaviour
{
    //public
    public bool emptyHanded = true;
    [HideInInspector] public bool carryingMais;
    [HideInInspector] public bool outOfSeeds = true;
    [HideInInspector] public bool outOfWater = true;
    [HideInInspector] public int currentWaterCarryAmount;
    public int currentSeedCarryAmount { get; set; }


    //private serialized
    [SerializeField] private int maxWaterCarryAmount;
    [SerializeField] private int maxSeedCarryAmount;
    [SerializeField] private GameObject mais;

    //private
    private Player woman;
    private Player man;

    private void Start()
    {
        if (GetComponent<Woman>())
        {
            woman = GetComponent<Woman>();
            UIManagerFarm.Instance.WaterGirl(currentWaterCarryAmount, maxWaterCarryAmount);
        }
        else
        {
            man = GetComponent<Man>();
            UIManagerFarm.Instance.SeedsBoy(currentSeedCarryAmount, maxSeedCarryAmount);
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

            woman.gameObject.GetComponent<Interactions>().enabled = true;
            woman.SetAnimation("waterIsEmpty", true);
        }
        else
        {
            outOfWater = false;

            woman.gameObject.GetComponent<Interactions>().enabled = false;
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
