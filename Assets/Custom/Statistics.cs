using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {
    public int MaxHunger = 14400;

    public int MaxSleep = 28800;

    public int MaxToilet = 14400;

    public int MaxJoy = 28800;

    private const string HUNGER = "HUNGER";
    private const string SLEEP = "SLEEP";
    private const string TOILET = "TOILET";
    private const string JOY = "JOY";
    private const string LAST_LOGIN = "LAST_LOGIN";

    public int CurrentHunger { get; private set; }

    public int CurrentSleep { get; private set; }

    public int CurrentToilet { get; private set; }

    public int CurrentJoy { get; private set; }

    private int _lastLogin;

    // Use this for initialization
    void Start() {
        CurrentHunger = PlayerPrefs.GetInt(HUNGER, MaxHunger);
        CurrentSleep = PlayerPrefs.GetInt(SLEEP, MaxSleep);
        CurrentToilet = PlayerPrefs.GetInt(SLEEP, MaxToilet);
        CurrentJoy = PlayerPrefs.GetInt(JOY, MaxJoy);
        _lastLogin = PlayerPrefs.GetInt(LAST_LOGIN, GetCurrentTimeInSeconds());

        int elaspedSeconds = GetCurrentTimeInSeconds() - _lastLogin;
        CurrentHunger -= elaspedSeconds;
        CurrentSleep -= elaspedSeconds;
        CurrentToilet -= elaspedSeconds;
        CurrentJoy -= elaspedSeconds;
        SaveToPrefs();

        StartCoroutine(ReduceStats());
    }

    // Update is called once per frame
    void Update() {

    }

    /**
    Używać tylko w metodzie Start z powodu dziwnego zachowania czasu na Androidzie
    */
    private int GetCurrentTimeInSeconds() {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long seconds = (long) (DateTime.UtcNow - epoch).TotalSeconds;
        return (int) seconds;
    }

    private IEnumerator ReduceStats() {
        while (true) {
            CurrentHunger--;
            CurrentSleep--;
            CurrentToilet--;
            CurrentJoy--;

            SaveToPrefs();
            yield return new WaitForSeconds(1f);
        }
    }

    private void SaveToPrefs() {
        PlayerPrefs.SetInt(HUNGER, CurrentHunger);
        PlayerPrefs.SetInt(SLEEP, CurrentSleep);
        PlayerPrefs.SetInt(TOILET, CurrentToilet);
        PlayerPrefs.SetInt(JOY, CurrentJoy);
        PlayerPrefs.SetInt(LAST_LOGIN, GetCurrentTimeInSeconds());
        PlayerPrefs.Save();
    }
}