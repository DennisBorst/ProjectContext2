using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChild : MonoBehaviour
{
    private Transform playerObject;
    private Transform previousParent;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            previousParent = collider.transform.parent;
            collider.transform.parent = transform;
            Debug.Log(previousParent);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Exit elevator");
            collider.transform.parent = previousParent;
        }
    }
}
