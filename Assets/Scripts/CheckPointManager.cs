using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Vector3 lastCheckPoint;
    public GameObject[] cinematic;
    [HideInInspector] public bool thisEnabled = false;
    public bool disableCinematic = false;

    [SerializeField] private int cinematicDisableCount;
    [SerializeField] private GameObject[] setActive;

    private static CheckPointManager instance;

    private void Awake()
    {
        instance = this;
        thisEnabled = true;

        DontDestroyOnLoad(instance);
    }

    public void DisableCinematic(int cinematicNumber)
    {
        cinematicDisableCount = cinematicNumber;
        disableCinematic = true;
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
