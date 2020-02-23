using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : Interactable
{
    //private serialized
    [SerializeField] private int maxFoodCarryAmount;
    [SerializeField] private float timeToCinematic;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject cinematic;

    //private
    private int currentFoodCarryAmount;
    private bool missionFinished = false;
    private FarmStats farmStats;

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (manCollding)
        {
            farmStats = collider.gameObject.GetComponent<FarmStats>();
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }

    private void Start()
    {
        UIManagerFarm.Instance.MaisChange(currentFoodCarryAmount, maxFoodCarryAmount);
        cinematic.SetActive(false);
    }

    private void Update()
    {
        if (manCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0) && !farmStats.emptyHanded)
            {
                farmStats.emptyHanded = true;
                man.SetAnimation("isHarvestingPlant", true);
                StartCoroutine(man.SetanimationBoolFalse("isHarvestingPlant", 1f));
                ChangeFoodNumber();
            }
        }

        if (missionFinished)
        {
            EndLevel();
        }

        if (maxFoodCarryAmount == 0)
        {
            missionFinished = true;
        }
    }

    public void ChangeFoodNumber()
    {
        currentFoodCarryAmount++;

        if (currentFoodCarryAmount >= maxFoodCarryAmount)
        {
            currentFoodCarryAmount = maxFoodCarryAmount;
            missionFinished = true;
        }

        UIManagerFarm.Instance.MaisChange(currentFoodCarryAmount, maxFoodCarryAmount);
    }

    private void EndLevel()
    {
        particles.SetActive(true);

        if (manCollding && womanCollding)
        {
            missionFinished = false;
            //GameManager.Instance.LoadNextLevel();
            anim.SetTrigger("FadeOut");
            StartCoroutine(WaitForCinematic());
        }
    }

    IEnumerator WaitForCinematic()
    {
        yield return new WaitForSeconds(timeToCinematic);
        cinematic.SetActive(true);
    }
}
