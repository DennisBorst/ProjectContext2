using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    private void Start()
    {
        ui.SetActive(false);
    }
    public void Toggle()
    {

        if (ui != null)
        {
            ui.SetActive(!ui.activeSelf);

            if (ui.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
            }
        }
    }
    public void MainMenu(string levelName)
    {
        Toggle();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(levelName);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    #region Singleton
    private static PauseMenu instance;
    private void Awake()
    {
        instance = this;
    }

    public static PauseMenu Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PauseMenu();
            }

            return instance;
        }
    }
    #endregion
}
