using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BindTimeToHatch : MonoBehaviour {
    private HatchingManager _hatchingManager;
    // Use this for initialization
    void Start() {
        _hatchingManager = GameObject.Find("HatchManager").GetComponent<HatchingManager>();
        StartCoroutine(UpdateTime());
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator UpdateTime() {
        while(true) {
            UpdateTimeLabel();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTimeLabel() {
        int minutes = (int) Math.Floor((double) (_hatchingManager.currentTimeToHatchInMiliseconds / 60 / 1000));
        int seconds = (int) (_hatchingManager.currentTimeToHatchInMiliseconds / 1000 - minutes * 60);
        gameObject.GetComponent<Text>().text = minutes + ":" + seconds;
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (hasFocus) {
            UpdateTimeLabel();
        }
    }
}
