using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainPlant : Interactable
{
    private bool collected = false;

    private void Update()
    {
        Debug.Log(ObtainedPlants.Instance.currentPlantCollected);
        if (womanCollding && woman.interact && !collected)
        {
            if(ObtainedPlants.Instance.currentPlantCollected == ObtainedPlants.Instance.amountOfPlantToCollect)
            {
                return;
            }

            ObtainedPlants.Instance.currentPlantCollected++;
            collected = true;

            woman.animationPlaying = true;
            woman.SetAnimation("isHarvestingPlant", true);
            StartCoroutine(woman.SetanimationBoolFalse("isHarvestingPlant", 1.8f));
            StartCoroutine(HarvestPlant(1.8f));
        }
    }

    IEnumerator HarvestPlant(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);
        Destroy(this.gameObject);
    }
}
