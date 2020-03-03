﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CinematicActivation : MonoBehaviour
{

    //private serialized
    [SerializeField] private float cinematicTime;
    [SerializeField] private bool firstCinematic;
    [SerializeField] private bool endCinematic;
    [SerializeField] private GameObject[] deActiveObject;
    [SerializeField] private GameObject[] objectsToDisableForEver;

    //private
    private float currentCinematicTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (firstCinematic)
        {
            if (CheckPointManager.Instance.disableCinematic)
            {
                this.gameObject.SetActive(false);
                return;
            }
        }

        currentCinematicTimer = cinematicTime;
        for (int i = 0; i < deActiveObject.Length; i++)
        {
            deActiveObject[i].SetActive(false);
        }

        for (int i = 0; i < objectsToDisableForEver.Length; i++)
        {
            objectsToDisableForEver[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentCinematicTimer = Timer(currentCinematicTimer);

        if(currentCinematicTimer <= 0)
        {
            if (endCinematic)
            {
                Destroy(CheckPointManager.Instance.gameObject);
                GameManager.Instance.LoadNextLevel();
                return;
            }

            for (int i = 0; i < deActiveObject.Length; i++)
            {
                deActiveObject[i].SetActive(true);
            }

            this.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
