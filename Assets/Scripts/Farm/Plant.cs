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
    [SerializeField] private MeshRenderer groundMeshRenderer;
    [SerializeField] private Material[] groundTextures; //0 is droog, 1 is nat

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

    //FMOD
    [FMODUnity.EventRef]
    public string PlantSeedEvent;
    [FMODUnity.EventRef]
    public string CutCropEvent;
    [FMODUnity.EventRef]
    public string PutDownSeedEvent;
    [FMODUnity.EventRef]
    public string WaterPlantEvent;

    // Start is called before the first frame update
    void Start()
    {
        ResetPlant();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (collider.gameObject.layer == 8)
        {
            farmStatsBoy = collider.gameObject.GetComponent<FarmStats>();
        }
        
        if(collider.gameObject.layer == 9)
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
            groundMeshRenderer.material = groundTextures[1];
            currentDeathtime = cropDeathTimer;
            GrowPlant();
        }
        else
        {
            groundMeshRenderer.material = groundTextures[0];
            currentDeathtime = Timer(currentDeathtime);

            if (currentDeathtime <= 0)
            {
                cropIsDead = true;
                plantStages[currentStage].SetActive(false);
                plantStages[currentStage + 3].SetActive(true);
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
                if (cropIsDead)
                {
                    ResetPlant();
                    return;
                }

                if (!planted && farmStatsBoy.currentSeedCarryAmount > 0 && !man.animationPlaying)
                {
                    ResetPlant();
                    PlantSeed();
                    return;
                }
                
                if (finished && !farmStatsBoy.carryingMais && !man.animationPlaying)
                {
                    //Get some food 
                    Debug.Log("You harvest a plant");
                    man.SetAnimation("isHarvestingPlant", true);
                    StartCoroutine(man.SetanimationBoolFalse("isHarvestingPlant", 1f));
                    StartCoroutine(HarvestPlant(1f));
                    FMODUnity.RuntimeManager.PlayOneShot(CutCropEvent, transform.position);
                    return;
                }
            }
        }

        //Woman Input
        if (womanCollding)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0) && !cropIsDead)
            {
                if(farmStatsGirl != null)
                {
                    if (farmStatsGirl.currentWaterCarryAmount > 0 && planted && !woman.animationPlaying)
                    {
                        currentWaterTime = waterTimer;
                        farmStatsGirl.ChangeWaterNumber(-waterNeeded);
                        woman.SetAnimation("giveWater", true);
                        StartCoroutine(woman.SetanimationBoolFalse("giveWater", 3.55f));
                        FMODUnity.RuntimeManager.PlayOneShot(WaterPlantEvent, transform.position);
                    }
                }
            }
        }
    }
    private void PlantSeed()
    {
        planted = true;
        plantStages[0].SetActive(true);
        man.animationPlaying = true;
        farmStatsBoy.ChangeSeedNumber(-1);
        man.SetAnimation("isPlacingSeed", true);
        StartCoroutine(man.SetanimationBoolFalse("isPlacingSeed", 1.4f));
        FMODUnity.RuntimeManager.PlayOneShot(PlantSeedEvent, transform.position);

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

    IEnumerator HarvestPlant(float duration)
    {
        yield return new WaitForSeconds(duration);
        farmStatsBoy.emptyHanded = false;
        ResetPlant();
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

        groundMeshRenderer.material = groundTextures[0];
    }
}