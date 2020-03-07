using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFood : Interactable
{
    public bool grabbingFood;
    public GameObject mais;

    private bool foodHasBeenTaken = false;

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
                grabbingFood = true;
                foodHasBeenTaken = true;
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
                grabbingFood = true;
                foodHasBeenTaken = true;
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
        grabbingFood = false;
    }
}
