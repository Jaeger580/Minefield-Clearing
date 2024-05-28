using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//ATW 5/23/24
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreenObject;
    [SerializeField] private TMP_Text winLoseText;
    static public GameOverScreen instance;

    private void Awake()
    {//Only making a singleton because it's extremely fast to prototype; we'd hook it up properly if we commit to the idea
        if (instance == null) instance = this;
        else Destroy(this);
    } 

    private void Start()
    {
        if (gameOverScreenObject.activeInHierarchy) gameOverScreenObject.SetActive(false);
    }

    public void DisplayGameOver(bool win)
    {
        //TimeController.ChangeTimeScale(0f);
        winLoseText.text = win ? "You win!" : "You lose!";
        gameOverScreenObject.SetActive(true);
    }

    [ContextMenu("Win")]
    private void ExampleWin()
    {//Unit test, basically
        DisplayGameOver(true);
    }

    [ContextMenu("Lose")]
    private void ExampleLose()
    {//Unit test, basically
        DisplayGameOver(false);
    }
}

static public class TimeController
{
    static public void ChangeTimeScale(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
}