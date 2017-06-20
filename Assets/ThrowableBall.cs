using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBall : MonoBehaviour {
    private bool _swiping;
    private Camera cam;
    private Vector2 oldPosition;
    private float elapsedTimeSinceLastTimeCheck;
    private Vector2 speed;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        _swiping = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void addVelocity()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Debug.Log("Current mouse position: " + currentPosition);
        Vector2 velocity = (currentPosition - oldPosition) * 15;
        Debug.Log("Old mouse position: " + oldPosition);
        GetComponent<Rigidbody2D>().velocity = velocity;
        Debug.Log("Ball velocity: " + velocity);

    }

    void FixedUpdate()
    {
        if (finishedSwiping())
        {
            _swiping = false;
            addVelocity();
            Debug.Log("Fixed time: " + Time.fixedDeltaTime);
        }
        if (_swiping)
        {
            Vector3 touchPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            //Debug.Log(mousePosition);
            float oldZ = transform.position.z;
            transform.position = new Vector3(touchPosition.x, touchPosition.y, oldZ);
        }
		if (elapsedTimeSinceLastTimeCheck > 0.1f)
		{
            oldPosition = new Vector2(transform.position.x, transform.position.y);
			elapsedTimeSinceLastTimeCheck = 0;
		}
		elapsedTimeSinceLastTimeCheck += Time.fixedDeltaTime;
    }

    private bool finishedSwiping()
    {
        return (Input.touchCount < 1 || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)) && _swiping;
    }

    void OnMouseDown() {
        Debug.Log("Swiping ball!");
        _swiping = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
