using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDataCollecter : MonoBehaviour
{
    [SerializeField] private float yAs;

    private void Update()
    {
        yAs = transform.rotation.y;
    }
}
