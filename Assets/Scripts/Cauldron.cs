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
    private bool womanOccupied;
    private bool manOccupied;
    private Vector2 directionCount;
    private Vector3 lookPos;

    private void Start()
    {
        directionCount = startDirection;
        slider.maxValue = amountOfRoundsToTurn;
    }

    private void Update()
    {
        if (womanCanInteract)
        {
            CheckWomanInput();
        }
    }

    private void CheckWomanInput()
    {
        if (womanCollding && !womanOccupied)
        {
            if (woman.interact)
            {
                womanOccupied = true;
                woman.animationPlaying = true;
                woman.SetAnimation("isWalking", false);
            }
        }

        if (womanOccupied)
        {
            if(woman.deinteract)
            {
                womanOccupied = false;
                woman.animationPlaying = false;
            }

            lookPos = potObject.transform.position - woman.playerObject.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
            float eulerY = lookRot.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, eulerY, 0);
            woman.playerObject.transform.rotation = Quaternion.Slerp(woman.playerObject.transform.rotation, rotation, Time.deltaTime * 5f);

            Vector2 direction = new Vector2(woman.inputX, woman.inputZ);

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

            if (currentRound >= amountOfRoundsToTurn)
            {
                Debug.Log("Congrats you are a master spinner");
                womanOccupied = false;
                woman.animationPlaying = false;
            }
        }
    }
}
