using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChild : MonoBehaviour
{
    private Transform playerObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        collider.transform.parent = transform;
    }
}
