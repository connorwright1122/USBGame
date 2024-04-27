using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject completeUI;

    public bool completeActive = false;
    [SerializeField]
    public Text accuracyText;
    [SerializeField]
    public Text timeText;
    [SerializeField]
    public Text finalScoreText;

    public MoveToNextLevel moveNL;


    void Start()
    {
        completeUI = transform.GetChild(0).gameObject;
        moveNL = GetComponent<MoveToNextLevel>();
    }

    void Update()
    {
        /*
        if (completeActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //
            }
        }
        */
    }

    public void ActivateCompleteUI()
    {
        completeActive = true;
        completeUI.SetActive(true);
        moveNL.SetNextScenePlayerPrefs();
        Cursor.lockState = CursorLockMode.Confined;
    }

    //public void SetScoreValues(float accuracy, string finalTime)
    public IEnumerator SetScoreValues(float accuracy, string finalTime)
    {
        //currentTimeText.text
        accuracyText.text = "Accuracy: " + accuracy.ToString();
        timeText.text = "Time: " + finalTime;
        yield return null;
    }
}
