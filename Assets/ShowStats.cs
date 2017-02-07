using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStats : MonoBehaviour {
    public float hiddenX = 80.6f;
    public float shownX = -76.7f;

    private bool shown = true;
    private ObjectManager ObjectManager;

    private void toggleShown() {
        shown = !shown;
    }

    // Use this for initialization
    void Start() {
        ObjectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
    }

    // Update is called once per frame
    void Update() { }

    public void ToggleBars() {
        Debug.Log("Toggling");
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation() {
        Debug.Log("Animation started!");
        bool tmpShown = shown;
        foreach (var StatBar in ObjectManager.BarsList) {
            Debug.Log("Starting animation for bar " + StatBar);
            StartCoroutine(ToggleBar(StatBar, tmpShown));
            yield return new WaitForSeconds(0.05f);
        }
        toggleShown();
        Debug.Log("Finished animation!");
    }

    private IEnumerator ToggleBar(GameObject statBar, bool shown) {
        RectTransform statBarTransform = statBar.GetComponent<RectTransform>();
        float oldY = statBarTransform.anchoredPosition.y;
        if (shown) {
            while (statBarTransform.anchoredPosition.x < hiddenX) {
                float oldX = statBarTransform.anchoredPosition.x;
                statBarTransform.anchoredPosition = new Vector2(oldX + 10, oldY);
                yield return new WaitForSeconds(0.005f);
            }
            //Correction
            statBarTransform.anchoredPosition = new Vector2(hiddenX, oldY);
        }
        else {
            while (statBarTransform.anchoredPosition.x > shownX) {
                float oldX = statBarTransform.anchoredPosition.x;
                statBarTransform.anchoredPosition = new Vector2(oldX - 10, oldY);
                Debug.Log(statBarTransform.anchoredPosition);
                yield return new WaitForSeconds(0.005f);
            }
            //Correction
            statBarTransform.anchoredPosition = new Vector2(shownX, oldY);
        }
    }
}