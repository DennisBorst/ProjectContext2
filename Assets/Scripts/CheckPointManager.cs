using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Vector3 lastCheckPoint;
    public GameObject[] cinematic;
    [HideInInspector] public bool thisEnabled = false;
    [HideInInspector] public bool disableCinematic = false;

    [SerializeField] private int cinematicDisableCount;
    [SerializeField] private GameObject[] setActive;

    private static CheckPointManager instance;

    private void Awake()
    {
        instance = this;
        thisEnabled = true;
        //cinematicObject = GameObject.FindGameObjectsWithTag("Cinematic");
        //cinematicObject[0].gameObject.SetActive(false);
        DontDestroyOnLoad(instance);
    }

    /*
    private void Update()
    {
        if(cinematic[0] == null)
        {
            for (int i = 0; i < setActive.Length; i++)
            {
                setActive[i].SetActive(true);
            }
        }
    }
    */

    public void DisableCinematic(int cinematicNumber)
    {
        cinematicDisableCount = cinematicNumber;
        disableCinematic = true;
        //Destroy(cinematic[cinematicNumber - 1]);
        //cinematic[cinematicNumber].GetComponent<CinematicActivation>().enabled = false;
    }
    public static CheckPointManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new CheckPointManager();
            }

            return instance;
        }
    }

}
