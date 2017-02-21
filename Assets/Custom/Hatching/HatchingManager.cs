﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class HatchingManager : MonoBehaviour {
    //Constants
    private const string EGG_ID = "EggId";

    private const string IS_HATCHED = "IsHatched";
    private const string TIME_TO_HATCH = "TimeToHatch";


    public long currentTimeToHatchInMiliseconds { get; private set; }
    public long _currentTimeInMiliseconds;
    public List<Sprite> AvailableEggSprites;

    private SpriteRenderer _curtainRenderer;
    private long _absoluteTimeToHatchInMiliseconds;
    private const float _hatchingTime = 10 * 60 * 1000; //10 minut, w milisekundach

    void Start() {
        _curtainRenderer = GameObject.Find("Curtain").GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt(IS_HATCHED) == 2) SceneManager.LoadScene("MainScene");
        Debug.Log("Started!");
	    PlayerPrefs.DeleteAll();
	    PlayerPrefs.Save();
        if (!IsHatchingOrHatched()) {
            PrepareNewHatching();
        }
        SetUpTimes();
        SetUpEgg();
        StartCoroutine("UpdateTime");
    }

    private void SetUpEgg() {
        int eggId = PlayerPrefs.GetInt(EGG_ID);
        GameObject.Find("Egg").GetComponent<SpriteRenderer>().sprite = AvailableEggSprites[eggId];
    }

    private void SetUpTimes() {
        _currentTimeInMiliseconds = GetCurrentTimeInMiliseconds();
        _absoluteTimeToHatchInMiliseconds = (long) PlayerPrefs.GetFloat(TIME_TO_HATCH);
        currentTimeToHatchInMiliseconds = _absoluteTimeToHatchInMiliseconds - _currentTimeInMiliseconds;
    }

    private void PrepareNewHatching() {
        PlayerPrefs.SetInt(EGG_ID, RandomizeEgg());
        PlayerPrefs.SetInt(IS_HATCHED, 1);
        PlayerPrefs.SetFloat(TIME_TO_HATCH, GetCurrentTimeInMiliseconds() + _hatchingTime);
        PlayerPrefs.Save();
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (hasFocus) {
            SetUpTimes();
        }
    }

    /**
    0 lub null - jajko jeszcze nie wyklute
    1 - jajko w trakcie wykluwania
    2 - jajko już wyklute
    */
    private bool IsHatchingOrHatched() {
        return PlayerPrefs.HasKey(IS_HATCHED) || PlayerPrefs.GetInt(IS_HATCHED, 0) != 0;
    }

    /**
    Używać tylko w metodzie Start z powodu dziwnego zachowania czasu na Androidzie
     */
    private long GetCurrentTimeInMiliseconds() {
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        long seconds = (long) (DateTime.UtcNow - epoch).TotalMilliseconds;
        return seconds;
    }

    // Update is called once per frame
    void Update() {
        if (PlayerPrefs.GetInt(IS_HATCHED) == 2) TransitToMainScene();
    }

    public void TransitToMainScene() {
        StartCoroutine(ShowCurtainAndLoadScene());
    }

    private IEnumerator ShowCurtainAndLoadScene() {
        DontDestroyOnLoad(GameObject.Find("Curtain"));
        while (_curtainRenderer.color.a < 1) {
            Color c = _curtainRenderer.color;
            c.a += 0.01f;
            _curtainRenderer.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator UpdateTime() {
        while (true) {
            _currentTimeInMiliseconds += 1000;
            Debug.Log("Updated time! ");
            currentTimeToHatchInMiliseconds = _absoluteTimeToHatchInMiliseconds - _currentTimeInMiliseconds;
            if (currentTimeToHatchInMiliseconds <= 0) PlayerPrefs.SetInt(IS_HATCHED, 2);
            yield return new WaitForSeconds(1f);
        }
    }

    private int RandomizeEgg() {
        Random rand = new Random();
        return rand.Next(0, AvailableEggSprites.Count);
    }
}