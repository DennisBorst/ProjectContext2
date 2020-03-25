using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotateToCam : MonoBehaviour
{
    [SerializeField] private GameObject camera;

    private void Start()
    {
        if(camera == null)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }
    private void Update()
    {
        if(camera != null)
        {
            this.transform.LookAt(camera.transform);
        }
    }
}
