using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void LoadPrototype()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void LoadSample()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadRoom()
    {
        SceneManager.LoadScene("Room1");
    }

    public void LoadMancave()
    {
        SceneManager.LoadScene("Mancave");
    }

    public void LoadOffice()
    {
        SceneManager.LoadScene("Office");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
