using System;

using UnityEngine;

public class TimeSvc : MonoBehaviour {
    public static TimeSvc Instance = null;
    private PETimer peTimer;

    public void InitService() {
        Instance = this;
        peTimer = new PETimer();
        peTimer.SetLog((str) => {
            Debug.Log("time log:" + str);
        });
        Debug.Log("Init TimerSvc");
    }
    private void Update() {
        peTimer.Update();
    }

    public int AddTimeTask(Action<int> callback, double delay, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1) {
        return peTimer.AddTimeTask(callback, delay, timeUnit, count);
    }

    public double GetNowTime() {
        return peTimer.GetMillisecondsTime();
    }

    public void DelTask(int tid) {
        peTimer.DeleteTimeTask(tid);
    }

}