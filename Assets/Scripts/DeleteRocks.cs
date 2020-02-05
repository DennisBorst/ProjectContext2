using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteRocks : Interactable
{
    [SerializeField] private GameObject rocks;

    private void Update()
    {
        if (womanCollding)
        {
            if (woman.interact)
            {
                Debug.Log("Delete Rocks");
                rocks.SetActive(false);
            }
        }
    }
    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }
    public override void SwitchBool(bool man)
    {
        base.SwitchBool(man);
    }
}
