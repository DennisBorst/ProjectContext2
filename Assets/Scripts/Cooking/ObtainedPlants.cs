using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObtainedPlants : MonoBehaviour
{
    public int amountOfPlantToCollect;
    public int currentPlantCollected { get; set; }

    [SerializeField] private GameObject tutorialObject;
    [SerializeField] private GameObject womanInteractParticle;
    [SerializeField] private TextMeshProUGUI woodText;

    private bool tutorialAlreadyActivated = false;

    public void Start()
    {
        tutorialObject.SetActive(false);
    }

    public void AddPlantNumber()
    {
        currentPlantCollected++;
        woodText.text = currentPlantCollected + "/" + amountOfPlantToCollect;

        if(currentPlantCollected == amountOfPlantToCollect && !tutorialAlreadyActivated)
        {
            tutorialAlreadyActivated = true;
            tutorialObject.SetActive(true);
        }

        if (currentPlantCollected == amountOfPlantToCollect)
        {
            womanInteractParticle.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        woodText.text = currentPlantCollected + "/" + amountOfPlantToCollect;
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
