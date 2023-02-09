using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool IsTimerOpen = false;
    private TextMeshProUGUI timerText;

    [SerializeField] private float time = 60;
    private float remainingTime;

    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        remainingTime = time;
    }

    
    void Update()
    {
        if(Config.LevelState == LevelState.Play && IsTimerOpen)
        {
            if (remainingTime > 0f)
            { remainingTime -= Time.deltaTime;
                timerText.text = "Time : " + Mathf.CeilToInt(remainingTime);
            }
            else
                Config.OnGameEnd?.Invoke();
        }
       
        
    }
}
