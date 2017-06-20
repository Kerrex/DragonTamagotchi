using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour {
    private Statistics _statistics;
    // Use this for initialization
    void Start () {
        _statistics = GameObject.Find("Dragon").GetComponent<Statistics>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown() {
        _statistics.CurrentSleep = _statistics.MaxSleep;
    }
}
