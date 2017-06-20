using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseJoyWhenMoving : MonoBehaviour {

    public int joyRisingRatio = 5;
    private GameObject _ball;
    private GameObject _dragon;
    private Vector3 lastPosition;

	// Use this for initialization
	void Start () {
        _dragon = GameObject.Find("Dragon");
        _ball = gameObject;
        updateLastPosition();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentBallPositon = _ball.transform.position;
        float distance = Vector2.Distance(new Vector2(lastPosition.x, lastPosition.y), new Vector2(currentBallPositon.x, currentBallPositon.y));
        if (distance > 0.1f) {
            _dragon.GetComponent<Statistics>().RaiseJoyBy(joyRisingRatio);
            updateLastPosition();
            Debug.Log("Joy updated! " + _dragon.GetComponent<Statistics>().CurrentJoy);
        }
	}

    private void updateLastPosition() {
        Vector3 tmpPosition = _ball.transform.position;
        lastPosition = new Vector3(tmpPosition.x, tmpPosition.y, tmpPosition.z);
    }
}
