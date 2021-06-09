using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    float currentTime;
    bool pause;
    [SerializeField] TextMeshProUGUI secondsText, centsText;

    void Awake() {
        ResetTime();
        pause = true;
    }

    void Update() {
        if (!pause) {
            currentTime += Time.deltaTime;
            UpdateTimeText();
        }
        
    }

    public void ResetTime() {
        currentTime = 0;
        UpdateTimeText();
    }

    public void PauseTime() { pause = true; }
    public void ResumeTime() { pause = false; }

    private  void UpdateTimeText() {
        string text = "" ;

        int seconds = Mathf.FloorToInt(currentTime);
        if (seconds < 10) { text += "0"; }
        text += seconds.ToString();

        secondsText.text = text;

        text = "";

        int cents = Mathf.FloorToInt((currentTime - seconds) * 100f);
        if (cents < 10) { text += "0"; }
        text += cents.ToString();

        centsText.text = text;
    }
}
