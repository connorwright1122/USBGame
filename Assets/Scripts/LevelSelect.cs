using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);

        for(int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelAt)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

}
