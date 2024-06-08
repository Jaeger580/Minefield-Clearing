using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text timerText;
    private float timer = 0f;

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        TimerUpdate();
    }

    private void TimerUpdate()
    {
        int _minutes = Mathf.FloorToInt(timer / 60);
        int _seconds = Mathf.FloorToInt(timer % 60);
        if (_minutes <= 0 && _seconds <= 0)
        {
            _minutes = 0;
            _seconds = 0;
        }
        string time = _minutes.ToString("0#") + ":" + _seconds.ToString("0#"); //Display the timer

        timerText.text = $"{time}";
    }
}
