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

    public int CurrentHunger { get; set; }

    public int CurrentSleep { get; set; }

    public int CurrentToilet { get; set; }

    public int CurrentJoy { get; set; }

    private int _lastLogin;

    // Use this for initialization
    void Start() {
        CurrentHunger = PlayerPrefs.GetInt(HUNGER, MaxHunger);
        CurrentSleep = PlayerPrefs.GetInt(SLEEP, MaxSleep);
        CurrentToilet = PlayerPrefs.GetInt(SLEEP, MaxToilet);
        CurrentJoy = PlayerPrefs.GetInt(JOY, MaxJoy);
        _lastLogin = PlayerPrefs.GetInt(LAST_LOGIN, GetCurrentTimeInSeconds());

        FixStatsAfterPause();
        SaveToPrefs();

        StartCoroutine("ReduceStats");
    }

    // Update is called once per frame

    void Update() {

    }

    void OnApplicationFocus(bool hasFocus) {
        // Trzeba pauzować z powodu możliwości nadpisania _lastLogin przed skorygowaniem statystyk
        if (hasFocus) {
            FixStatsAfterPause();
            StartCoroutine("ReduceStats");
        }
        else {
            StopCoroutine("ReduceStats");
        }
    }

    public void RaiseJoyBy(int pointsToRaise) {
        CurrentJoy += pointsToRaise;
        if (CurrentJoy > MaxJoy) {
            CurrentJoy = MaxJoy;
        }
    }

    public void RaiseToiletBy(int pointsToRaise)
    {
        CurrentToilet += pointsToRaise;
        if (CurrentToilet > MaxToilet)
        {
            CurrentToilet = MaxToilet;
        }
    }

    public void RaiseHungerBy(int pointsToRaise)
    {
        StartCoroutine(RaiseHungerCourutine(pointsToRaise));
    }

    private IEnumerator RaiseHungerCourutine(int pointsToRaise)
    {
        for (int i = 0; i < pointsToRaise; i++)
        {
            CurrentHunger++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void FixStatsAfterPause() {
        int elaspedSeconds = GetCurrentTimeInSeconds() - _lastLogin;
        CurrentHunger -= elaspedSeconds;
        CurrentSleep -= elaspedSeconds;
        CurrentToilet -= elaspedSeconds;
        CurrentJoy -= elaspedSeconds;

        if (CurrentHunger < 0) CurrentHunger = 0;
        if (CurrentSleep < 0) CurrentSleep = 0;
        if (CurrentToilet < 0) CurrentSleep = 0;
        if (CurrentJoy < 0) CurrentJoy = 0;
    }

    /**
    Używać rzadko z powodu dziwnego zachowania czasu na Androidzie
    */
    private int GetCurrentTimeInSeconds() {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long seconds = (long) (DateTime.UtcNow - epoch).TotalSeconds;
        return (int) seconds;
    }

    private IEnumerator ReduceStats() {
        while (true) {
            if (CurrentHunger > 0) CurrentHunger--;
            if (CurrentSleep > 0) CurrentSleep--;
            if (CurrentToilet > 0) CurrentToilet--;
            if (CurrentJoy > 0) CurrentJoy--;

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