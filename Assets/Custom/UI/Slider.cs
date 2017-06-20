using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Slider : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float value;

    public Image ImageReference;

    void Update() {
        ImageReference.fillAmount = value;
    }
}
