using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text TimerText;
    public float LevelTimeSecs = 0;

    void Start()
    {
        GameController.Instance.OnGameResumed += ResetTimer;
    }

    private void OnDisable()
    {
        GameController.Instance.OnGameResumed -= ResetTimer;
    }

    public void ResetTimer()
    {
        LevelTimeSecs = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.GamePaused)
        {
            LevelTimeSecs += Time.deltaTime;
        }
        TimerText.text = GetFormattedTime();
    }

    public string GetFormattedTime()
    {
        var time = LevelTimeSecs;
        var mins = Mathf.FloorToInt(time / 60);
        var secs = Mathf.FloorToInt(time) - mins * 60;
        var milis = Mathf.FloorToInt(1000 * (time - Mathf.FloorToInt(time)));
        return string.Format("{0:D2}:{1:D2}:{2:D2}", mins, secs, milis);
    }
}
