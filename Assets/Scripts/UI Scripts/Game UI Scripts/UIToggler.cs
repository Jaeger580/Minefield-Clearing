using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    [SerializeField] private GameObject uiToToggle;
    static public bool uiIsOn;
    private IEnumerator Start()
    {
        yield return null;
        uiIsOn = false;
        uiToToggle.SetActive(false);
    }

    public void ToggleUI()
    {
        if (GameOverScreen.instance.gameIsOver) return;

        uiIsOn = !uiToToggle.activeInHierarchy;
        uiToToggle.SetActive(uiIsOn);
        if(uiIsOn)
            Cursor.lockState = CursorLockMode.Confined;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
