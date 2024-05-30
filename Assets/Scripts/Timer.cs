using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public bool countDown = true;
    public float maxTime = 60.0f;
    public bool finite = true;
    public bool startOnAwake = false;

    public UnityEvent onTimerFinished;

    public bool IsRunning;
    public float CurrentTime;

    private bool isCoroutineRunning = false;

    public playerHealth[] playerHealthScripts; // Array to store references to PlayerHealth scripts for both players

    void Start()
    {
        if (startOnAwake)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (IsRunning)
        {
            if (countDown)
            {
                CurrentTime -= Time.deltaTime;
                if (CurrentTime <= 0.0f)
                {
                    CurrentTime = 0.0f;
                    TimerFinished();
                }
            }
            else
            {
                CurrentTime += Time.deltaTime;
                if (finite && CurrentTime >= maxTime)
                {
                    CurrentTime = maxTime;
                    TimerFinished();
                }
            }
        }
    }

    public void StartTimer()
    {
        IsRunning = true;
        CurrentTime = countDown ? maxTime : 0.0f;
    }

    public void StopTimer()
    {
        IsRunning = false;
    }

    public void FinishTimer()
    {
        CurrentTime = countDown ? 0.0f : maxTime;
        TimerFinished();
    }

    private IEnumerator TimerFinishedCoroutine()
    {
        isCoroutineRunning = true;
        IsRunning = false;
        onTimerFinished.Invoke();

        // Custom logic for timer finished coroutine

        yield return null; // Ensure at least one frame has passed
        isCoroutineRunning = false;
    }

    private void TimerFinished()
    {
        if (!isCoroutineRunning)
        {
            StartCoroutine(TimerFinishedCoroutine());

            // Reduce health for both players
            for (int i = 0; i < playerHealthScripts.Length; i++)
            {
                playerHealthScripts[i].ReduceHealthToZero();
            }
        }
    }



    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(CurrentTime / 60.0f);
        int seconds = Mathf.FloorToInt(CurrentTime % 60.0f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
