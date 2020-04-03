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
    [SerializeField] private GameObject disableParticle;
    [SerializeField] private GameObject cinematic;
    [SerializeField] private GameObject[] cornObjects;

    //private
    private int currentFoodCarryAmount;
    private bool missionFinished = false;
    private FarmStats farmStats;

    //FMOD
    [FMODUnity.EventRef]
    public string PutDownCropEvent;

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if (manCollding)
        {
            farmStats = collider.gameObject.GetComponent<FarmStats>();
        }
    }

    private void Start()
    {
        UIManagerFarm.Instance.MaisChange(currentFoodCarryAmount, maxFoodCarryAmount);
        if(cinematic != null)
        {
            cinematic.SetActive(false);
        }

        for (int i = 0; i < cornObjects.Length; i++)
        {
            cornObjects[i].SetActive(false);
        }
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
                FMODUnity.RuntimeManager.PlayOneShot(PutDownCropEvent, transform.position);
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
        cornObjects[currentFoodCarryAmount - 1].SetActive(true);

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
        disableParticle.SetActive(false);

        if (manCollding && womanCollding)
        {
            missionFinished = false;
            //GameManager.Instance.LoadNextLevel();
            if(anim != null)
            {
                anim.SetTrigger("FadeOut");
            }
            StartCoroutine(WaitForCinematic());
        }
    }

    IEnumerator WaitForCinematic()
    {
        yield return new WaitForSeconds(timeToCinematic);
        cinematic.SetActive(true);
    }
}
