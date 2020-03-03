using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    //private serialized
    [SerializeField] private float restartDelay;

    //private
    private Player currentPlayer;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            currentPlayer = collider.GetComponent<Player>();
            currentPlayer.animationPlaying = true;
            GameManager.Instance.Reload(restartDelay);
        }
    }
}
