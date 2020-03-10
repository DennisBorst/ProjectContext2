using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFood : Interactable
{
    public GameObject mais;

    private bool foodHasBeenTaken = false;
    private GrabCheck grabCheck;

    private void Update()
    {
        if (foodHasBeenTaken)
        {
            return;
        }

        if(manCollding)
        {
            if (man.interact)
            {
                foodHasBeenTaken = true;
                grabCheck = man.GetComponent<GrabCheck>();
                grabCheck.grabbingFood = true;
                man.SetAnimation("isHarvestingPlant", true);
                StartCoroutine(man.SetanimationBoolFalse("isHarvestingPlant", 1f));
                StartCoroutine(GrabTime());
                mais.SetActive(false);
            }
        }

        if (womanCollding)
        {
            if (woman.interact)
            {
                foodHasBeenTaken = true;
                grabCheck = woman.GetComponent<GrabCheck>();
                grabCheck.grabbingFood = true;
                woman.SetAnimation("isHarvestingPlant", true);
                StartCoroutine(woman.SetanimationBoolFalse("isHarvestingPlant", 1f));
                StartCoroutine(GrabTime());
                mais.SetActive(false);
            }
        }
    }

    IEnumerator GrabTime()
    {
        yield return new WaitForSeconds(1f);
        FoodManager.Instance.AddFoodNumber();
        grabCheck.grabbingFood = false;
    }
}
