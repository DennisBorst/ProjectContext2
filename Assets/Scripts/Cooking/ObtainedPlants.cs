using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainedPlants : MonoBehaviour
{
    public int amountOfPlantToCollect;
    public int currentPlantCollected { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddPlantNumber()
    {
        currentPlantCollected++;
    }

    #region Singleton
    private static ObtainedPlants instance;

    private void Awake()
    {
        instance = this;
    }

    public static ObtainedPlants Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObtainedPlants();
            }

            return instance;
        }
    }
    #endregion
}
