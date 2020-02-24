using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSwitch : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [Header("Rotation")]
    [Range(0, 0.1f)]
    [SerializeField] private float rotationChangeSpeed;
    [SerializeField] private Vector3 rotationLight;
    [Header("Color")]
    [Range(0, 0.1f)]
    [SerializeField] private float colorChangeSpeed;
    [SerializeField] private Color32 colorLight;
    [Header("Light intensity")]
    [Range(0, 0.1f)]
    [SerializeField] private float intensityChangeSpeed;
    [SerializeField] private float newIntensity;

    private bool switchLight = false;
    private Vector3 currentRotation;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            switchLight = true;
        }
    }

    private void Update()
    {
        if (switchLight)
        {
            SwitchLightningSettings();
        }
    }

    private void SwitchLightningSettings()
    {
        Quaternion rotation = Quaternion.Euler(rotationLight);
        directionalLight.transform.rotation = Quaternion.Lerp(directionalLight.transform.rotation, rotation, rotationChangeSpeed);

        directionalLight.color = Color32.Lerp(directionalLight.color, colorLight, colorChangeSpeed);
        directionalLight.intensity = Mathf.Lerp(directionalLight.intensity, newIntensity, intensityChangeSpeed);
    }
}
