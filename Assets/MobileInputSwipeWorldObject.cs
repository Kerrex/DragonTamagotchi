using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MobileInputSwipeWorldObject : MonoBehaviour
{
    private bool _isSwiping = false;
    private Vector2 _startSwipingPosition;
    private Transform _transform;
    private Touch _touch;
    private bool _movingCourutineWorking = false;
    private Camera cam;

    public enum Axis
    {
        X_AXIS,
        Y_AXIS
    }

    public Axis SelectedAxis;
    public float MinXValue;
    public float MaxXValue;

    //TODO GESTY

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedAxis.Equals(Axis.X_AXIS))
        {
            if (_isSwiping && !MovingTowardsLeftBoundary() && !MovingTowardsRightBoundary())
            {
                SwipeObjectByXAxis();
            }
        }
        else
        {
            if (_isSwiping && !MovingTowardsTopBoundary() && !MovingTowardsBottomBoundary())
            {
                SwipeObjectByYAxis();
            }
        }
    }

    //private void DetectSwiping()
    //{

    //    // Start swiping if finger touches this object
    //    if ()
    //    {
    //        _isSwiping = true;

    //    }

    //    // Stops swiping if no finger touches the screen
    //    if (Input.touchCount == 0 && _isSwiping)
    //    {
    //        _isSwiping = false;
    //        if (!_movingCourutineWorking) StartCorrectionCourutine();
    //    }
    //}

    void OnMouseDown()
    {
        _isSwiping = true;
        Debug.Log("Swiping!");
    }

    void OnMouseUp()
    {
        _isSwiping = false;
        if (!_movingCourutineWorking) StartCorrectionCourutine();
        Debug.Log("Stopped swiping!");
    }

    private void StartCorrectionCourutine()
    {
        if (SelectedAxis.Equals(Axis.X_AXIS))
        {
            if (IsCloserToLeftSide())
            {
                StartCoroutine(MoveToXAxisBoundary(true));
            }
            else
            {
                StartCoroutine(MoveToXAxisBoundary(false));
            }
        }
        else
        {
            if (IsCloserToBottomSide())
            {
                StartCoroutine(MoveToYAxisBoundary(true));
            }
            else
            {
                StartCoroutine(MoveToYAxisBoundary(false));
            }
        }
    }

    //private bool IsGameObjectHit()
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    results.ForEach(result => Debug.Log(result));
    //    return results.Select(result => result.gameObject.GetComponentInParent<MobileInputSwipeUI>()).Contains(this);
    //}



    /**
     *   Methods for X axis swipe only
     */

    private void SwipeObjectByXAxis()
    {
        Vector3 touchPosition = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        _transform.position = new Vector2(touchPosition.x,
                                          _transform.position.y);
        CorrectXPosition();
    }

    private IEnumerator MoveToXAxisBoundary(bool moveToleft)
    {
        float oldY = _transform.localPosition.y;
        _movingCourutineWorking = true;
        if (moveToleft)
        {
            while (_transform.localPosition.x > MinXValue)
            {
                float oldX = _transform.localPosition.x;
                _transform.localPosition = new Vector2(oldX - 0.1f, oldY);
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            while (_transform.localPosition.x < MaxXValue)
            {
                float oldX = _transform.localPosition.x;
                _transform.localPosition = new Vector2(oldX + 0.1f, oldY);
                yield return new WaitForSeconds(0.005f);
            }
        }
        _movingCourutineWorking = false;
        CorrectXPosition();
    }

    private void CorrectXPosition()
    {
        if (_transform.localPosition.x < MinXValue)
        {
            _transform.localPosition = new Vector2(MinXValue, _transform.localPosition.y);
        }
        else if (_transform.localPosition.x > MaxXValue)
        {
            _transform.localPosition = new Vector2(MaxXValue, _transform.localPosition.y);
        }
    }

    private bool MovingTowardsRightBoundary()
    {
        return _transform.localPosition.x >= MaxXValue && Input.GetTouch(0).deltaPosition.x > 0;
    }

    private bool MovingTowardsLeftBoundary()
    {
        return _transform.localPosition.x <= MinXValue && Input.GetTouch(0).deltaPosition.x < 0;
    }

    private bool IsCloserToLeftSide()
    {
        return _transform.localPosition.x - MinXValue <= MaxXValue - _transform.localPosition.x;
    }

    public bool IsOnLeftSide()
    {
        return Math.Abs(_transform.localPosition.x - MinXValue) < 0.01f;
    }

    public bool IsOnRightSide()
    {
        return Math.Abs(_transform.localPosition.x - MaxXValue) < 0.01f;
    }


    /**
     *   Methods for Y Axis Swipe only
     */

    private void SwipeObjectByYAxis()
    {
        float deltaPositionY = Input.GetTouch(0).deltaPosition.y;
        float newPositionY = _transform.position.y + deltaPositionY;
        _transform.position = new Vector2(_transform.position.x,
                                          newPositionY);
        CorrectYPosition();

    }

    private IEnumerator MoveToYAxisBoundary(bool moveToBottom)
    {
        float oldX = _transform.localPosition.x;
        _movingCourutineWorking = true;
        if (moveToBottom)
        {
            while (_transform.localPosition.y > MinXValue)
            {
                float oldY = _transform.localPosition.y;
                _transform.localPosition = new Vector2(oldX, oldY - 0.1f);
                yield return new WaitForSeconds(0.005f);
            }
        }
        else
        {
            while (_transform.localPosition.y < MaxXValue)
            {
                float oldY = _transform.localPosition.y;
                _transform.localPosition = new Vector2(oldX, oldY + 0.1f);
                yield return new WaitForSeconds(0.005f);
            }
        }
        _movingCourutineWorking = false;
        CorrectYPosition();
    }

    private void CorrectYPosition()
    {
        if (_transform.localPosition.y < MinXValue)
        {
            _transform.localPosition = new Vector2(_transform.localPosition.x, MinXValue);
        }
        else if (_transform.localPosition.y > MaxXValue)
        {
            _transform.localPosition = new Vector2(_transform.localPosition.x, MaxXValue);
        }
    }

    private bool MovingTowardsTopBoundary()
    {
        return _transform.localPosition.y >= MaxXValue && Input.GetTouch(0).deltaPosition.y > 0;
    }

    private bool MovingTowardsBottomBoundary()
    {
        return _transform.localPosition.y <= MinXValue && Input.GetTouch(0).deltaPosition.y < 0;
    }

    private bool IsCloserToBottomSide()
    {
        return _transform.localPosition.y - MinXValue <= MaxXValue - _transform.localPosition.y;
    }

    public bool IsAtTop()
    {
        return Math.Abs(_transform.localPosition.y - MaxXValue) < 0.01f && SelectedAxis == Axis.Y_AXIS;
    }

    public bool IsAtBottom()
    {
        return Math.Abs(_transform.localPosition.y - MinXValue) < 0.01f && SelectedAxis == Axis.Y_AXIS;
    }

}
