using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndQuicklyShowEggAndIsland : MonoBehaviour {

    private SpriteRenderer _renderer;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 0f);
        StartCoroutine(ShowEgg());
	}

    private IEnumerator ShowEgg()
    {
        yield return new WaitForSeconds(0.5f);
        while (_renderer.color.a < 1) {
            _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a + 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
