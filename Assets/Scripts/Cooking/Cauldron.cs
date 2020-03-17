using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cauldron : Interactable
{
    //private serialized
    [SerializeField] private int amountOfRoundsToTurn;
    [SerializeField] private bool womanCanInteract;
    [SerializeField] private bool manCanInteract;
    [SerializeField] private Vector2 startDirection;
    [SerializeField] private GameObject potObject;
    [SerializeField] private Slider slider;

    //private
    private int currentRound;
    private bool occupied;
    private bool ingredientsAdded;
    private bool colliding;
    private Player player;
    private Vector2 directionCount;
    private Vector3 lookPos;

    private void Start()
    {
        directionCount = startDirection;
        slider.maxValue = amountOfRoundsToTurn;

        if (womanCanInteract)
        {
            player = woman;
        }

    }

    private void Update()
    {
        if (!ingredientsAdded)
        {
            Debug.Log("Not enough ingredients");

            if (womanCollding && woman.interact && ObtainedPlants.Instance.currentPlantCollected == ObtainedPlants.Instance.amountOfPlantToCollect)
            {
                ingredientsAdded = true;
                ObtainedPlants.Instance.currentPlantCollected = 0;

                //woman.SetAnimation("isHarvestingPlant", true);
                //StartCoroutine(woman.SetanimationBoolFalse("isHarvestingPlant", 1.5f));
            }
            return;
        }

        if (womanCanInteract)
        {
            colliding = womanCollding;
            CheckInput();
        }

        if (!womanCanInteract)
        {
            colliding = manCollding;
            CheckInput();
        }
    }

    private void CheckInput()
    {
        if (colliding && !occupied)
        {
            if (player.interact)
            {
                occupied = true;
                player.animationPlaying = true;
                player.SetAnimation("isWalking", false);
            }
        }

        if (occupied)
        {
            if(player.deinteract)
            {
                occupied = false;
                player.animationPlaying = false;
            }

            lookPos = potObject.transform.position - player.playerObject.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
            float eulerY = lookRot.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
            player.playerObject.transform.rotation = Quaternion.Slerp(player.playerObject.transform.rotation, rotation, Time.deltaTime * 5f);

            Vector2 direction = new Vector2(player.inputX, player.inputZ);

            if(direction.x <= -0.8f && directionCount.x == -1)
            {
                directionCount.x = 0;
                directionCount.y = 1;
            }
            else if(direction.y >= 0.8f && directionCount.y == 1)
            {
                directionCount.x = 1;
                directionCount.y = 0;
            }
            else if(direction.x >= 0.8f && directionCount.x == 1)
            {
                directionCount.x = 0;
                directionCount.y = -1;
            }
            else if(direction.y <= -0.8f && directionCount.y == -1)
            {
                directionCount.x = -1;
                directionCount.y = 0;
                currentRound++;
                slider.value = currentRound;
            }

            //Animations
            if(Mathf.Abs(direction.x) > 0.5f || Mathf.Abs(direction.y) > 0.5f)
            {
                player.SetAnimation("isCooking", true);
            }
            else
            {
                player.SetAnimation("isCooking", false);
            }

            if (currentRound >= amountOfRoundsToTurn && player == woman)
            {
                occupied = false;
                womanCanInteract = false;
                player.animationPlaying = false;
                player = man;
                currentRound = 0;
                slider.value = 0;

                ingredientsAdded = false;
            }

            if(currentRound >= amountOfRoundsToTurn && player == man)
            {

            }
        }
    }
}
