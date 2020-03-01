using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDataCollecter : MonoBehaviour
{
    [SerializeField] private float yAs;
    //[SerializeField] private CinemachineBrain cinemachineBrain;
    //private Transform cam;

    private void Update()
    {
        yAs = transform.eulerAngles.y;

        /*
        if(yAs > 120)
        {
            yAs = transform.rotation.y * 160;
        }
        else
        {
            yAs = transform.rotation.y * 130;
        }
        */
        //yAs = cam.rotation.y;
        //yAs = m_WorldUpOverride.rotation.y * 130;
        ///yAs = cinemachineBrain.transform.rotation.y;
    }
}
