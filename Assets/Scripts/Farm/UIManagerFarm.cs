using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManagerFarm : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wheatBoy;
    [SerializeField] private TextMeshProUGUI seedsBoy;
    [Space]
    [SerializeField] private TextMeshProUGUI waterGirl;

    public void SeedsBoy(int amount, int maxAmount)
    {
        seedsBoy.text = amount + "/" + maxAmount;
    }

    public void WaterGirl(int amount, int maxAmount)
    {
        waterGirl.text = amount + "/" + maxAmount;
        
    }

    public void MaisChange(int wheat, int maxWheat)
    {
        wheatBoy.text = wheat + "/" + maxWheat;
    }

    #region Singleton
    private static UIManagerFarm instance;
    private void Awake()
    {
        instance = this;
    }

    public static UIManagerFarm Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManagerFarm();
            }

            return instance;
        }
    }
    #endregion
}
