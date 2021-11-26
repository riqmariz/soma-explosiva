using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public static class Timers
{
    public static void CreateTicker(GameObject obj, float ttl, float delta, UnityAction act) {
        var timer_object = obj.AddComponent<TickerObject>();
        timer_object.TimeToLive = ttl;
        timer_object.Delay = delta;
        timer_object.Action = act;
        timer_object.DelayCount = delta;
    }
    
    public static void CreateTickerUnscaledTime(GameObject obj, float ttl, float delta, UnityAction act,UnityAction endAct) {
        var timer_object = obj.AddComponent<TickerObjectUnscaledTime>();
        timer_object.TimeToLive = ttl;
        timer_object.Delay = delta;
        timer_object.Action = act;
        timer_object.DelayCount = delta;
        timer_object.EndAction = endAct;
    }

    public static void CreateFixedTickerCondition(GameObject obj, Func<bool> function, UnityAction act, [CanBeNull] UnityAction endAct = null, string message = "")
    {
        var fixedTickerCondition = obj.AddComponent<FixedTickerObjectCondition>();
        fixedTickerCondition.boolFunction = function;
        fixedTickerCondition.Action = act;
        fixedTickerCondition.EndAction = endAct;
        fixedTickerCondition.message = message;
    }

    public static void CreateClock(GameObject obj, float clock_time, UnityAction initial_act, UnityAction end_act)
    {
        var clock_object = obj.AddComponent<ClockObject>();
        clock_object.Duration = clock_time;
        clock_object.StartAction = initial_act;
        clock_object.EndAction = end_act;
    }

    public static TimerObject CreateTimer(GameObject obj)
    {
        var timer_object = obj.AddComponent<TimerObject>();
        return timer_object;
    }
}

internal class TickerObject : MonoBehaviour
{
    public float TimeToLive { get; set; }
    public float Delay { get; set; }
    public UnityAction Action { get; set; }
    public float DelayCount { get; set; }

    private float _timeAlive = 0f;

    private void Update()
    {
        if (_timeAlive > TimeToLive)
        {
            Debug.Log("Timer Finished!");
            Destroy(this);
        }

        if (DelayCount >= Delay)
        {
            Action.Invoke();
            DelayCount = 0f;
        }

        DelayCount += Time.deltaTime;
        _timeAlive += Time.deltaTime;
    }
}

internal class TickerObjectUnscaledTime : MonoBehaviour
{
    public float TimeToLive { get; set; }
    public float Delay { get; set; }
    public UnityAction Action { get; set; }
    
    public UnityAction EndAction { get; set; }
    public float DelayCount { get; set; }

    private float _timeAlive = 0f;
    private void Update()
    {
        if (_timeAlive > TimeToLive)
        {
            Debug.Log("Timer Finished!");
            EndAction.Invoke();
            Destroy(this);
            return;
        }

        if (DelayCount >= Delay)
        {
            Action.Invoke();
            DelayCount = 0f;
        }

        DelayCount += Time.unscaledDeltaTime;
        _timeAlive += Time.unscaledDeltaTime;
    }
}

internal class FixedTickerObjectCondition : MonoBehaviour
{
    public UnityAction Action { get; set; }
    [CanBeNull] public UnityAction EndAction { get; set; }
    public Func<bool> boolFunction;
    public string message;

    private bool _destroyingTicker;

    private float _time;

    private void Start()
    {
        _time = Time.time;
    }

    public void FixedUpdate()
    {
        if (boolFunction.Invoke() && !_destroyingTicker) //run the action, while true
        {
            Action.Invoke();
        }
        else //destroys when it get false
        {
            EndAction?.Invoke();
            _time = Time.time - _time;
           //Debug.Log(message+" Ticker Finished!"+" time to finish: "+_time);
            Destroy(this);
            _destroyingTicker = true; //destroy has a delay
        }
    }
}

internal class ClockObject : MonoBehaviour
{
    public float Duration { get; set; }
    public UnityAction StartAction { get; set; }
    public UnityAction EndAction { get; set; }

    private float currentTime;
    
    private void Start()
    {
        currentTime = 0;
        StartAction?.Invoke();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= Duration)
        {
            EndAction?.Invoke();
            Destroy(this);
        }
    }
}

public class TimerObject : MonoBehaviour
{
    public float startedTime { get; private set; }
    public float runningTime { get; private set; }
    public float elapsedTime
    {
        get
        {
            return runningTime - startedTime;
        }
    }

    private void Start()
    {
        startedTime = Time.time;
    }

    private void Update()
    {
        runningTime += Time.deltaTime;
    }

    public void FinishTimer()
    {
        Destroy(this);
    }
}