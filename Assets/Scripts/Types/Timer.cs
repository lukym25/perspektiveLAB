using System;
using UnityEngine;

public class Timer
{
    public float RemainingTime;
    public Action OnCompletionEvent;

    public Timer(float time)
    {
        RemainingTime = time;
    }

    public void CountDown()
    {
        if (RemainingTime <= 0) { return; }

        RemainingTime -= Time.deltaTime;

        if (RemainingTime <= 0)
        {
            RemainingTime = 0;
            OnCompletionEvent?.Invoke();
        }
    }
}
