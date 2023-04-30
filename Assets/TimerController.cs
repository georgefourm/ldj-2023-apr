using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text TimerText;
    protected float levelTimeSecs = 0;
    public float LastLevelTimeSecs = 0;

    private void Start()
    {
        GameController.Instance.OnGamePaused += ResetTimer;
    }

    public void ResetTimer()
    {
        LastLevelTimeSecs = levelTimeSecs;
        levelTimeSecs = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.GamePaused)
        {
            levelTimeSecs += Time.deltaTime;
        }
        TimerText.text = GetFormattedTime(levelTimeSecs);
    }

    public string GetFormattedTime(float time)
    {
        var mins = Mathf.RoundToInt(time / 60);
        var secs = Mathf.RoundToInt(time) - mins * 60;
        return string.Format("{0:D2}:{1:D2}", mins, secs);
    }
}
