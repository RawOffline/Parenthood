using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject panel;
    private List<GameObject> blackBorders = new List<GameObject>();

    void Start()
    {
        GameObject[] blackBordersArray = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.CompareTag("BlackBorders")).ToArray();
        blackBorders.AddRange(blackBordersArray);
    }

    void Update()
    {
        foreach (GameObject blackBorder in blackBorders)
        {
            Debug.Log(blackBorder);
        }

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
        foreach (GameObject obj in blackBorders)
        {
            obj.SetActive(true);
        }
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
        foreach (GameObject obj in blackBorders)
        {
            obj.SetActive(false);
        }
        Paused = true;
    }
}
