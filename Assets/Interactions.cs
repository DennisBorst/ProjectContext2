using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] private Player otherPlayer;

    [SerializeField] private string[] animationBool;
    [SerializeField] private float[] animationTime;
    [SerializeField] private int[] animationTriggerRange;

    //private
    private Player thisPlayer;
    private int randomNumber;

    private void Start()
    {
        thisPlayer = GetComponent<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && !thisPlayer.animationPlaying)
        {
            thisPlayer.animationPlaying = true;
            randomNumber = Random.Range(0, animationBool.Length);
            thisPlayer.SetAnimation(animationBool[randomNumber], true);
            StartCoroutine(thisPlayer.SetanimationBoolFalse(animationBool[randomNumber], animationTime[randomNumber]));
        }
    }
}
