using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodManager : MonoBehaviour
{
    //private serialized
    [SerializeField] private int foodToCollect;
    [SerializeField] private GameObject lastFood;
    [SerializeField] private GrabFood lastFoodScript;
    [SerializeField] private TextMeshProUGUI foodCollectedUI;
    [SerializeField] private GameObject[] objectsToSetActive; //Objects that needs to set active after the last mais has been taken

    //private
    private int currentFoodCollected;

    void Start()
    {
        lastFood.SetActive(false);
        lastFoodScript.enabled = false;

        foodCollectedUI.text = currentFoodCollected + "/" + (foodToCollect + 1);

        for (int i = 0; i < objectsToSetActive.Length; i++)
        {
            objectsToSetActive[i].SetActive(false);
        }
    }

    public void AddFoodNumber()
    {
        currentFoodCollected++;
        foodCollectedUI.text = currentFoodCollected + "/" + (foodToCollect + 1);

        if (currentFoodCollected == foodToCollect)
        {
            lastFood.SetActive(true);
            lastFoodScript.enabled = true;
        }

        if(currentFoodCollected == foodToCollect + 1)
        {
            for (int i = 0; i < objectsToSetActive.Length; i++)
            {
                objectsToSetActive[i].SetActive(true);
            }

            GetComponent<FMODUnity.StudioEventEmitter>().Play();

            this.enabled = false;
        }
    }

    #region Singleton
    private static FoodManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static FoodManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new FoodManager();
            }
            return instance;
        }
    }
    #endregion
}
