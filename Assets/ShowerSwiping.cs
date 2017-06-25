using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShowerSwiping : MonoBehaviour
{

    private Vector2 _initialPosition;
    public float MaxXInclination;
    public float MaxYInclination;	

    private bool _swiping;

    private Vector2 _lastMousePosition;

    private Vector2 _offset;
    // Use this for initialization
    void Start () {
        _initialPosition = new Vector2(transform.position.x, transform.position.y);
    }
    
    // Update is called once per frame
    void Update () {
        if (_swiping)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(touchPosition.x - _offset.x, touchPosition.y - _offset.y, transform.position.z);
            CorrectPositions();
        }
    }

    private void CorrectPositions()
    {
        var newX = transform.position.x;
        var newY = transform.position.y;

        if (newX > _initialPosition.x + MaxXInclination)
        {
            newX = _initialPosition.x + MaxXInclination;
        } else if (newX < _initialPosition.x - MaxXInclination)
        {
            newX = _initialPosition.x - MaxXInclination;
        }

        if (newY > _initialPosition.y + MaxYInclination)
        {
            newY = _initialPosition.y + MaxYInclination;
        } else if (newY < _initialPosition.y - MaxYInclination)
        {
            newY = _initialPosition.y - MaxYInclination;
        }
        
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void OnMouseDown()
    {
        _swiping = true;
        _offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseUp()
    {
        _swiping = false;
        StartCoroutine(ReturnToInitialPosition());
    }

    private IEnumerator ReturnToInitialPosition()
    {
        while (NotInInitialPosition())
        {
            transform.position = 
                Vector3.MoveTowards(transform.position, 
                                    new Vector3(_initialPosition.x, _initialPosition.y, transform.position.z), 
                                    5f * Time.deltaTime);
            yield return new WaitForSeconds(0.001f);
        }
    }

    public bool NotInInitialPosition()
    {
        return Math.Abs(transform.position.x - _initialPosition.x) > 0.01f &&
               Math.Abs(transform.position.y - _initialPosition.y) > 0.01f;
    }
}
