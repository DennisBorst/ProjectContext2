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
    private float distance;
    private float damping = 5f;
    private int randomNumber;
    private Player thisPlayer;
    private KeyCode playerButton;

    private List<string> currentBools = new List<string>();
    private List<float> currentTime = new List<float>();

    private void Start()
    {
        thisPlayer = GetComponent<Player>();

        if (GetComponent<Man>())
        {
            playerButton = KeyCode.Joystick1Button2;
        }

        if (GetComponent<Woman>())
        {
            playerButton = KeyCode.Joystick2Button2;
        }
    }
    private void Update()
    {
        distance = Vector3.Distance(thisPlayer.gameObject.transform.position, otherPlayer.gameObject.transform.position);
        
        if (Input.GetKeyDown(playerButton) && !thisPlayer.animationPlaying)
        {
            thisPlayer.animationPlaying = true;
            currentBools.Clear();
            currentTime.Clear();

            for (int i = 0; i < animationBool.Length; i++)
            {
                if (distance > 0)
                {
                    if (animationTriggerRange[i] > 0)
                    {
                        currentBools.Add(animationBool[i]);
                        currentTime.Add(animationTime[i]);
                    }
                }

                /*
                //Far range
                if (distance > 10)
                {
                    if(animationTriggerRange[i] > 10)
                    {
                        currentBools.Add(animationBool[i]);
                        currentTime.Add(animationTime[i]);
                    }
                }
                //Middle range
                else if (distance < 10 && distance > 5)
                {
                    if (animationTriggerRange[i] < 10 && animationTriggerRange[i] > 5)
                    {
                        currentBools.Add(animationBool[i]);
                        currentTime.Add(animationTime[i]);
                    }
                }
                //Close range
                else if (distance < 5)
                {
                    if (animationTriggerRange[i] < 5)
                    {
                        currentBools.Add(animationBool[i]);
                        currentTime.Add(animationTime[i]);
                    }
                }
                */
            }

            randomNumber = Random.Range(0, currentBools.Count);
            thisPlayer.SetAnimation(currentBools[randomNumber], true);
            StartCoroutine(thisPlayer.SetanimationBoolFalse(currentBools[randomNumber], currentTime[randomNumber]));
        }
    }
}
