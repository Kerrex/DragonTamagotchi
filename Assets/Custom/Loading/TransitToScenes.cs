using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitToScenes : MonoBehaviour {
    public Slider slider;
    private const string IS_HATCHED = "IsHatched";
	// Use this for initialization
	void Start () {
	    StartLoading();
	    if (PlayerPrefs.GetInt(IS_HATCHED) == 2) {
	        TransitToScene("MainScene");
	    }
	    else {
	        TransitToScene("EggHatching");
	    }
	}

    private void StartLoading() {
        StartCoroutine(LoadingCourutine());
    }

    private IEnumerator LoadingCourutine() {
        while (slider.value < 1) {
            slider.value += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void TransitToScene(string sceneName) {
        StartCoroutine(WaitAndTransitCourutine(sceneName));
    }

    private IEnumerator WaitAndTransitCourutine(string scenename) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scenename);
    }
    // Update is called once per frame
	void Update () {
		
	}
}
