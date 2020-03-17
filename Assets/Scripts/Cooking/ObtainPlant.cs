using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainPlant : Interactable
{


    private void Update()
    {
        Debug.Log(ObtainedPlants.Instance.currentPlantCollected);
        if (womanCollding && woman.interact)
        {
            if(ObtainedPlants.Instance.currentPlantCollected == ObtainedPlants.Instance.amountOfPlantToCollect)
            {
                Debug.Log("You have enough plants collected");
            }
            else
            {
                ObtainedPlants.Instance.currentPlantCollected++;
                Destroy(this.gameObject);
            }
        }
    }
}
