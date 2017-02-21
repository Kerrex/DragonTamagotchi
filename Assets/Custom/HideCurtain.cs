using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCurtain : MonoBehaviour {
    private GameObject _curtain;

	// Use this for initialization
	void Start () {
		_curtain = GameObject.Find("Curtain");
	    if (_curtain != null) StartCoroutine(IEHideCurtain());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator IEHideCurtain() {
        Vector3 scale = _curtain.transform.localScale;
        _curtain.transform.localScale = new Vector3(scale.x * 5, scale.y * 5, scale.z);
        SpriteRenderer _curtainRenderer = _curtain.GetComponent<SpriteRenderer>();
        while (_curtainRenderer.color.a > 0) {
            Color c = _curtainRenderer.color;
            c.a -= 0.01f;
            _curtainRenderer.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(_curtain);
    }
}
