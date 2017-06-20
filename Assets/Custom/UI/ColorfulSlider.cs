using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ColorfulSlider : MonoBehaviour {

    private Slider _slider;
    private Image _fillObject;
    
    [Serializable]
    public struct ColorInterval {
        public float MinValue;
        public float MaxValue;
        public Color Color;
    }

    public ColorInterval[] ColorIntervals;

	// Use this for initialization
	void Start () {
	    _fillObject = GameObject.Find(gameObject.name + "/Fill Area/Fill").GetComponent<Image>();
	    _slider = gameObject.GetComponent<Slider>();
	    StartCoroutine(UpdateColor());
	}
	
	// Update is called once per frame
	void Update () {

	}

    private IEnumerator UpdateColor() {
        while (true) {
            foreach (var colorInterval in ColorIntervals) {
                if (_slider.value >= colorInterval.MinValue && _slider.value < colorInterval.MaxValue) {
                    _fillObject.color = new Color(colorInterval.Color.r, colorInterval.Color.g, colorInterval.Color.b);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
