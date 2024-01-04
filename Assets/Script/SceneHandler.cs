
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameObject mother;
    private MotherMovement motherMovement;
    private bool isGodMode = false;
    CheckpointManager checkpoint;
    public int constantFrame;

    //[SerializeField] Animator transitionAnim;
    public SceneTransition sceneTransition;

    void Start()
    {
        // Initial checkpoint
        //checkpointPosition = transform.position;
        checkpoint = GetComponent<CheckpointManager>();
        mother = GameObject.FindWithTag("Player");
        motherMovement = mother.GetComponent<MotherMovement>();
        Application.targetFrameRate = constantFrame;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextScene();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PreviousScene();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCheckpoint();
            //checkpoint.SetCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Respawn();
            //checkpoint.LoadCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleGodMode();
        }

        if (isGodMode)
        {
            motherMovement.HandleGodModeMovement();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Time.timeScale -= 0.25f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Time.timeScale += 0.25f;
        }
    }

    public void Restart()
    {
        // Ensure that scenes are added to the build settings 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextScene()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        //transitionAnim.SetTrigger("End");

        if (sceneTransition != null)
        {
            sceneTransition.PlayEndTransition();
        }
        yield return new WaitForSeconds(1);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // If the current scene is the last one, it wraps around to the first scene.
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        SoundManager.StopAllSounds();
        SceneManager.LoadScene(nextSceneIndex);

        //transitionAnim.SetTrigger("Start");
        if (sceneTransition != null)
        {
            sceneTransition.PlayStartTransition();
        }
    }

    public void PreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // If the current scene is the first one, it wraps around to the last scene.
        int previousSceneIndex = (currentSceneIndex - 1 + SceneManager.sceneCountInBuildSettings)
            % SceneManager.sceneCountInBuildSettings;

        SceneManager.LoadScene(previousSceneIndex);
    }

    public void ToggleGodMode()
    {
        isGodMode = !isGodMode;

        if (motherMovement != null)
        {
            if (isGodMode)
            {
                motherMovement.EnableGodMode();
            }
            else
            {
                motherMovement.DisableGodMode();
            }
        }

    }


    public void SetCheckpoint()
    {
        //checkpointPosition = transform.position;
        //Debug.Log("Checkpoint set at: " + checkpointPosition);
    }

    public void Respawn()
    {
        //transform.position = checkpointPosition;
        //Debug.Log("Respawned at checkpoint: " + checkpointPosition);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    /* TO DO
     * U - Show/Hide UI (turn on/off renderer components)
     * Alpha 1 - Invincible Player (turn on/off)
     * Alpha 2 - Ability to move through walls and fly (turn on/off)
     * Alpha 3 - Kill all enemies
     * Alpha 4 - Lower Time.timeScale by 0.25
     * Alpha 5 - Increase Time.timeScale by 0.25 */
}
