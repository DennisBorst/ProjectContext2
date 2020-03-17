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

    //private
    private int currentFoodCollected;

    void Start()
    {
        lastFood.SetActive(false);
        lastFoodScript.enabled = false;

        foodCollectedUI.text = currentFoodCollected + "/" + (foodToCollect + 1);
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
