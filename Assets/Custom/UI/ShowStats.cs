using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStats : MonoBehaviour {
    public float HiddenX = 80.6f;
    public float ShownX = -76.7f;

    private bool _shown = true;
    private ObjectManager _objectManager;

    private void toggleShown() {
        _shown = !_shown;
    }

    // Use this for initialization
    void Start() {
        _objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    // Update is called once per frame
    void Update() { }

    public void ToggleBars() {
        Debug.Log("Toggling");
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation() {
        Debug.Log("Animation started!");
        bool tmpShown = _shown;
        foreach (var statBar in _objectManager.BarsList) {
            Debug.Log("Starting animation for bar " + statBar);
            StartCoroutine(ToggleBar(statBar, tmpShown));
            yield return new WaitForSeconds(0.05f);
        }
        toggleShown();
        Debug.Log("Finished animation!");
    }

    private IEnumerator ToggleBar(GameObject statBar, bool shown) {
        RectTransform statBarTransform = statBar.GetComponent<RectTransform>();
        float oldY = statBarTransform.anchoredPosition.y;
        if (shown) {
            while (statBarTransform.anchoredPosition.x < HiddenX) {
                float oldX = statBarTransform.anchoredPosition.x;
                statBarTransform.anchoredPosition = new Vector2(oldX + 10, oldY);
                yield return new WaitForSeconds(0.005f);
            }
            //Correction
            statBarTransform.anchoredPosition = new Vector2(HiddenX, oldY);
        }
        else {
            while (statBarTransform.anchoredPosition.x > ShownX) {
                float oldX = statBarTransform.anchoredPosition.x;
                statBarTransform.anchoredPosition = new Vector2(oldX - 10, oldY);
                Debug.Log(statBarTransform.anchoredPosition);
                yield return new WaitForSeconds(0.005f);
            }
            //Correction
            statBarTransform.anchoredPosition = new Vector2(ShownX, oldY);
        }
    }
}