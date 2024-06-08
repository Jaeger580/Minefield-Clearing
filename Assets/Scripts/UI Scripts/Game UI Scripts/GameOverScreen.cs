using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

//ATW 5/23/24
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreenObject, uiParentObject, gridViewUIObject;
    [SerializeField] private TMP_Text winLoseText;
    static public GameOverScreen instance;
    [HideInInspector] public bool gameIsOver;

    // Used to disable player controlls on death
    [SerializeField]
    private GameObject player;

    private void Awake()
    {//Only making a singleton because it's extremely fast to prototype; we'd hook it up properly if we commit to the idea
        TimeController.ChangeTimeScale(1f);
        if (instance == null) instance = this;
        else Destroy(this);
    } 

    private void Start()
    {
        if (gameOverScreenObject.activeInHierarchy) gameOverScreenObject.SetActive(false);
        gameIsOver = false;
    }

    private void OnDestroy()
    {
        TimeController.ChangeTimeScale(1f);
    }

    public void DisplayGameOver(bool win)
    {
        gameIsOver = true;
        Cursor.lockState = CursorLockMode.Confined;
        TimeController.ChangeTimeScale(0f);
        //TimeController.ChangeTimeScale(0f);
        winLoseText.text = win ? "You win!" : "You lose!";
        gridViewUIObject.SetActive(false);
        gameOverScreenObject.SetActive(true);
        uiParentObject.SetActive(true);

        var controls = player.GetComponent<PlayerInput>();
        controls.actions.FindActionMap("Player").Disable();
        controls.actions.FindActionMap("UI").Enable();
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