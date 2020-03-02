using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContraWeight : MonoBehaviour
{
    [SerializeField] private Transform contraWeight;
    [SerializeField] private GameObject ropeContraWeight;
    [SerializeField] private GameObject ropeElevator;

    private Vector3 currentScale;
    private Elevator elevator;

    private void Awake()
    {
        elevator = GetComponent<Elevator>();
        currentScale = ropeContraWeight.transform.localScale;
    }

    void Update()
    {
        
    }
}
