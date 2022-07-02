using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public bool canPause = true;

    public GameObject pauseUI;
    public MovementOnlyForward player;
    
    void Start()
    {
        pauseUI = transform.GetChild(0).gameObject;
        player = GameObject.FindWithTag("Player").GetComponent<MovementOnlyForward>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
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
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        player.UnfreezePlayer();
        pauseUI.SetActive(false);
        isPaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
