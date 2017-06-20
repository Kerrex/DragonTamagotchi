using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Statistics))]
public class BindStatisticsToSlider : MonoBehaviour {
    private Statistics _statistics;

    public Slider HungerSlider;

    public Slider SleepSlider;

    public Slider ToiletSlider;

    public Slider JoySlider;
	// Use this for initialization
	void Start () {
	    _statistics = GetComponent<Statistics>();
	    StartCoroutine(BindStatsToSlider());
	}
	
	// Update is called once per frame
    // ReSharper disable PossibleLossOfFraction
	void Update () {

	}

    private IEnumerator BindStatsToSlider() {
        while (true) {
            HungerSlider.value = (float)_statistics.CurrentHunger / (float)_statistics.MaxHunger;
            SleepSlider.value = (float)_statistics.CurrentSleep / _statistics.MaxSleep;
            ToiletSlider.value = (float)_statistics.CurrentToilet / _statistics.MaxToilet;
            JoySlider.value = (float)_statistics.CurrentJoy / _statistics.MaxJoy;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
