using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public bool canPause = true;

    public GameObject pauseUI;
    public MovementV2 player;
    
    void Start()
    {
        pauseUI = transform.GetChild(0).gameObject;
        //player = GameObject.FindWithTag("Player").GetComponent<MovementOnlyForward>();
        player = GameObject.FindWithTag("Player").GetComponent<MovementV2>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canPause)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        player.FreezePlayer();
        pauseUI.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        player.UnfreezePlayer();
        pauseUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMenu()
    {
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("MainMenu");
    }
}
