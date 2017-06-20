using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleXPosition : MonoBehaviour
{

    public float X1;
    public float X2;
    public bool isMoving { get; set; }

    // Use this for initialization
    void Start()
    {
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartToggling() {
        isMoving = true;
        StartCoroutine(Toggle());
    }

    private IEnumerator Toggle()
    {
        float oldY = transform.localPosition.y;
        if (!IsPositionAtX1())
        {
            while (transform.localPosition.x > X1)
            {
                float oldX = transform.localPosition.x;
                transform.localPosition = new Vector2(oldX - 10f, oldY);
                yield return new WaitForSeconds(0.001f);
            }
            transform.localPosition = new Vector2(X1, oldY);
        }
        else
        {
            while (transform.localPosition.x < X2)
            {
                float oldX = transform.localPosition.x;
                transform.localPosition = new Vector2(oldX + 10f, oldY);
                yield return new WaitForSeconds(0.001f);
            }
            transform.localPosition = new Vector2(X2, oldY);
        }
        isMoving = false;
    }

    public bool IsPositionAtX1()
    {
        return Math.Abs(transform.localPosition.x - X1) < 0.5f;
    }
}
