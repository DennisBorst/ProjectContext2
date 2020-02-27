using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //private serialized
    [SerializeField] private int cinematicToDisable;
    [SerializeField] private GameObject cpm;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(cpm != null)
            {
                cpm.SetActive(true);
                CheckPointManager.Instance.DisableCinematic(cinematicToDisable);
                //firstCheckPoint = true;
            }

            CheckPointManager.Instance.lastCheckPoint = transform.position;
        }
    }
}
