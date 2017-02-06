using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStats : MonoBehaviour {
    public float hiddenX = 234.6f;
    public float shownX = 85.4f;

    private bool shown = false;
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
            StartCoroutine(ToggleBar(StatBar, shown));
            yield return new WaitForSeconds(0.3f);
        }
        toggleShown();
        Debug.Log("Finished animation!");
    }

    private IEnumerator ToggleBar(GameObject statBar, bool shown) {
        if (shown) {
            RectTransform statBarTransform = statBar.GetComponent<RectTransform>();
            while (statBarTransform.localPosition.x < hiddenX) {
                float oldX = statBarTransform.localPosition.x;
                float oldY = statBarTransform.localPosition.y;
                statBarTransform.localPosition.Set(oldX + 5, oldY, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else {
            RectTransform statBarTransform = statBar.GetComponent<RectTransform>();
            while (statBarTransform.localPosition.x > hiddenX) {
                Debug.Log(statBarTransform.localPosition);
                float oldX = statBarTransform.localPosition.x;
                float oldY = statBarTransform.localPosition.y;
                statBarTransform.localPosition.Set(oldX + 5, oldY, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}