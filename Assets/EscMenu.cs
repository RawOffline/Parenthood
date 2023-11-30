using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject panel;
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {

                Pause();
            }
        }



    }
    public void SwitchToMainMenu()
    {
        Time.timeScale = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
        Paused = false;

    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        //Application.Quit();
    }

    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        Paused = true;
    }
}
