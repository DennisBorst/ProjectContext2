using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool manCollding;
    [SerializeField] private bool womenCollding;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (manCollding)
        {
            if (Man.Instance.interact)
            {
                Debug.Log("Standing next to a rock");
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(collider.gameObject.layer == 8)
            {
                manCollding = true;
            }
            else if(collider.gameObject.layer == 9)
            {
                womenCollding = true;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.layer == 8)
            {
                manCollding = false;
            }
            else if (collider.gameObject.layer == 9)
            {
                womenCollding = false;
            }
        }
    }
}
