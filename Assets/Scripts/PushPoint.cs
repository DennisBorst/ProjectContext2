using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPoint : MonoBehaviour
{
    private Player player;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if (GetComponent<Man>())
            {

            }
        }
    }
}
