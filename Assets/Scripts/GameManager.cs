using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float nextLevelDelay;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelDelay());
    }
    IEnumerator LoadNextLevelDelay()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #region Singleton
    private static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }
    }
    #endregion
}
