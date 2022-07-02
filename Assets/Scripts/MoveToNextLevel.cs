using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextLevel : MonoBehaviour
{
    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1; 
    }

    //activate on button click
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneLoad);
    }

    public void SetNextScenePlayerPrefs()
    {
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
