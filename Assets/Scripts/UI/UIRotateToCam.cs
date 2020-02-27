using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotateToCam : MonoBehaviour
{
    //private
    private GameObject camera;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    private void Update()
    {
        this.transform.LookAt(camera.transform);
    }
}
