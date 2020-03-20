using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //public bool deinteractPossible;

    protected bool manCollding;
    protected bool womanCollding;

    protected Man man;
    protected Woman woman;

    private void Awake()
    {
        man = FindObjectOfType<Man>();
        woman = FindObjectOfType<Woman>();
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(collider.gameObject.layer == 8)
            {
                SwitchBool(true);
                //manCollding = true;
            }
            else if(collider.gameObject.layer == 9)
            {
                SwitchBool(false);
                //womanCollding = true;
            }
        }
    }

    public virtual void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.layer == 8)
            {
                SwitchBool(true);
                //manCollding = false;
            }
            else if (collider.gameObject.layer == 9)
            {
                SwitchBool(false);
                //womanCollding = false;
            }
        }
    }

    public virtual void SwitchBool(bool man)
    {
        if (man)
        {
            manCollding = !manCollding;
        }
        else
        {
            womanCollding = !womanCollding;
        }
    }
    
}
