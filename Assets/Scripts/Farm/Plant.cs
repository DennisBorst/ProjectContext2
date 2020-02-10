using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Interactable
{
    //private serialezed
    [SerializeField] private int waterNeeded;
    [SerializeField] private int amountOfStages;

    [SerializeField] private float nextStageTime;
    [SerializeField] private float waterTimer;
    [SerializeField] private float cropDeathTimer;

    [SerializeField] private GameObject[] plantStages;

    //private
    private int currentStage;

    private bool planted;
    private bool finished;
    private bool isColliding = false;
    private bool cropIsDead;

    private float currentNextStageTime;
    private float currentWaterTime;
    private float currentDeathtime;

    private FarmStats farmStatsBoy;
    private FarmStats farmStatsGirl;

    // Start is called before the first frame update
    void Start()
    {
        ResetPlant();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (manCollding)
        {
            farmStatsBoy = collider.gameObject.GetComponent<FarmStats>();
        }
        else
        {
            farmStatsGirl = collider.gameObject.GetComponent<FarmStats>();
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
    void Update()
    {
        CheckInput();

        if (cropIsDead || !planted)
        {
            return;
        }

        if (currentStage == (amountOfStages - 1))
        {
            finished = true;
            return;
        }

        GrowCheck();
    }

    private void GrowCheck()
    {
        if (currentWaterTime > 0)
        {
            currentWaterTime = Timer(currentWaterTime);
            GrowPlant();
        }
        else
        {
            currentDeathtime = Timer(currentDeathtime);

            if (currentDeathtime <= 0)
            {
                cropIsDead = true;
                plantStages[currentStage].SetActive(false);
                plantStages[plantStages.Length - 1].SetActive(true);
            }
        }
    }

    private void CheckInput()
    {
        //Man Input
        if (manCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Debug.Log("Clicking");

                if (cropIsDead)
                {
                    ResetPlant();
                    return;
                }

                if (!planted && !farmStatsBoy.outOfSeeds)
                {
                    ResetPlant();
                    PlantSeed();
                    farmStatsBoy.ChangeSeedNumber(-1);
                    return;
                }
                
                if (finished && !farmStatsBoy.carryingMais)
                {
                    //Get some food 
                    Debug.Log("You harvest a plant");
                    farmStatsBoy.emptyHanded = false;
                    ResetPlant();
                    return;
                }
            }
        }

        //Woman Input
        if (womanCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                if (!farmStatsGirl.outOfWater && planted)
                {
                    currentWaterTime = waterTimer;
                    farmStatsGirl.ChangeWaterNumber(-waterNeeded);
                }
            }
        }
    }
    private void PlantSeed()
    {
        planted = true;
        plantStages[0].SetActive(true);
    }

    private void GrowPlant()
    {
        currentNextStageTime = Timer(currentNextStageTime);

        if (currentNextStageTime <= 0)
        {
            plantStages[currentStage].SetActive(false);
            currentStage++;
            plantStages[currentStage].SetActive(true);

            currentNextStageTime = nextStageTime;
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    private void ResetPlant()
    {
        planted = false;
        finished = false;
        cropIsDead = false;

        for (int i = 0; i < plantStages.Length; i++)
        {
            plantStages[i].SetActive(false);
        }

        currentStage = 0;
        currentNextStageTime = nextStageTime;
        currentWaterTime = 0;
        currentDeathtime = cropDeathTimer;
    }
}