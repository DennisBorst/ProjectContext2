using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float nextLevelDelay;
    [SerializeField] private Animator fadeOutAnim;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload(0.5f);
        }
    }
    public void Reload(float reloadDelay)
    {
        StartCoroutine(ReloadDelay(reloadDelay));
    }
    IEnumerator ReloadDelay(float duration)
    {
        fadeOutAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelDelay());
    }
    public void LoadNextLevelFade()
    {
        fadeOutAnim.SetTrigger("FadeOut");
        StartCoroutine(LoadNextLevelDelay());
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadIndexLevel(int buildindex)
    {
        fadeOutAnim.SetTrigger("FadeOut");
        StartCoroutine(LoadIndexLevelDelay(buildindex));
    }
    IEnumerator LoadNextLevelDelay()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        NextLevel();
    }

    IEnumerator LoadIndexLevelDelay(int buildindex)
    {
        yield return new WaitForSeconds(nextLevelDelay);
        SceneManager.LoadScene(buildindex);

    }

    public void Quit()
    {
        Application.Quit();
    }

    #region Singleton
    private static GameManager instance;
    private void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
