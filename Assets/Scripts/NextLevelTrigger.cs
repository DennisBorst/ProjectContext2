using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : Interactable
{
    [SerializeField] private bool fade;
    [SerializeField] private Animator animFade;
    private void Update()
    {
        if(manCollding && womanCollding)
        {
            if (fade)
            {
                animFade.SetTrigger("FadeOut");
            }
            if (CheckPointManager.Instance != null)
            {
                Destroy(CheckPointManager.Instance.gameObject);
            }

            GameManager.Instance.LoadNextLevel();
        }
    }
}
