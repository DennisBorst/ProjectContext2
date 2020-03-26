using System.Collections;
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
    [SerializeField] private GameObject[] activateObject;

    //private
    private float currentCinematicTimer;

    //FMOD
    [SerializeField] private GameObject[] fmodDisableEvents;


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
            if (fmodDisableEvents != null)
            {
                for (int i = 0; i < fmodDisableEvents.Length; i++)
                {
                    fmodDisableEvents[i].GetComponent<FMODUnity.StudioEventEmitter>().Play();
                }
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

        for (int i = 0; i < activateObject.Length; i++)
        {
            activateObject[i].SetActive(true);
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
                if(CheckPointManager.Instance != null)
                {
                    Destroy(CheckPointManager.Instance.gameObject);
                }
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
