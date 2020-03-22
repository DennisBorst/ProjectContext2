using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : Interactable
{
    void Update()
    {
        if(manCollding && womanCollding)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
}
