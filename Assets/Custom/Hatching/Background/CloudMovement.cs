using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

    public float xLeft;
    public float xRight;
	public float timeInSeconds;
	public Direction direction;

    private float _pixelsPerStep;


	// Use this for initialization
	void Start () {
        if (direction.Equals(Direction.RIGHT)) {
			_pixelsPerStep = (xRight - xLeft) / timeInSeconds / 20;         
        } else {
            _pixelsPerStep = (xLeft - xRight) / timeInSeconds / 20;
        }
        StartCoroutine(MoveCloud());
	}

    private IEnumerator MoveCloud()
    {
        while(true) {
            Vector3 currentVector = GetComponent<Transform>().position;
            float currentX = currentVector.x;
            currentX += _pixelsPerStep;
            GetComponent<Transform>().position = new Vector3(currentX, currentVector.y, currentVector.z);

            if (direction.Equals(Direction.RIGHT) && currentX >= xRight) {
                GetComponent<Transform>().position = new Vector3(xLeft, currentVector.y, currentVector.z);
            }
            if (direction.Equals(Direction.LEFT) && currentX <= xLeft) {
                GetComponent<Transform>().position = new Vector3(xRight, currentVector.y, currentVector.z);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

	public enum Direction {
		LEFT,
		RIGHT
	}
}
