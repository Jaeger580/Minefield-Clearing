using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggler : MonoBehaviour
{
    [SerializeField] private GameObject uiToToggle;
    private IEnumerator Start()
    {
        yield return null;
        uiToToggle.SetActive(false);
    }

    public void ToggleUI()
    {
        uiToToggle.SetActive(!uiToToggle.activeInHierarchy);
    }
}
