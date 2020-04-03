using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerWithoutReload : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    [SerializeField] private GameObject[] players;
    [SerializeField] private Animator anim;

    private Vector3 cpm;
    private Player currentPlayer;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            currentPlayer = collider.GetComponent<Player>();
            currentPlayer.animationPlaying = true;
            anim.SetTrigger("FadeOut");
            StartCoroutine(FadeTime());
        }
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSeconds(fadeDuration - 1f);
        cpm = CheckPointManager.Instance.lastCheckPoint;
        players[0].transform.position = new Vector3(cpm.x, cpm.y, cpm.z + 0.5f);
        players[1].transform.position = new Vector3(cpm.x, cpm.y, cpm.z - 0.5f);
        yield return new WaitForSeconds(1f);
        players[0].GetComponent<Player>().animationPlaying = false;
        players[1].GetComponent<Player>().animationPlaying = false;
        anim.SetTrigger("FadeIn");
    }
}
