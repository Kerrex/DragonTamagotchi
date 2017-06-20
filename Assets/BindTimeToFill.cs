using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BindTimeToFill : MonoBehaviour {

    private HatchingManager _hatchingManager;
    private Slider _slider;

	// Use this for initialization
	void Start () {
	    _hatchingManager = GameObject.Find("HatchManager").GetComponent<HatchingManager>();
	    _slider = GetComponent<Slider>();
	    StartCoroutine(UpdateTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator UpdateTime() {
        while(true) {
            UpdateClock();
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateClock() {
        float percentToHatch = _hatchingManager.currentTimeToHatchInMiliseconds / _hatchingManager._hatchingTime;
        _slider.value = percentToHatch;
        Debug.Log("Time to hatch: " + _hatchingManager.currentTimeToHatchInMiliseconds);
        Debug.Log("Hatching time: " + _hatchingManager._hatchingTime);
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (hasFocus) {
            UpdateClock();
        }
    }
}
