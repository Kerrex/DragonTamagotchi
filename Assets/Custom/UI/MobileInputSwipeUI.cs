using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputSwipeUI : MonoBehaviour {
    private bool _isSwiping = false;
    private Vector2 _startSwipingPosition;
    private RectTransform _transform;
    private Touch _touch;
    private bool _movingCourutineWorking = false;

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
    void Start() {
        _transform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
		DetectSwiping();

        if (SelectedAxis.Equals(Axis.X_AXIS)) {
			if (_isSwiping && !MovingTowardsLeftBoundary() && !MovingTowardsRightBoundary()) {
				SwipeObjectByXAxis();
			}         
        } else {
            if (_isSwiping && !MovingTowardsTopBoundary() && !MovingTowardsBottomBoundary()) {
                SwipeObjectByYAxis();
            }
        }
    }

    private void DetectSwiping() {

        // Start swiping if finger touches this object
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began /*Input.GetMouseButtonDown(0)*/) {
            if (IsGameObjectHit()) {
                _isSwiping = true;
            }
        }

        // Stops swiping if no finger touches the screen
        if (Input.touchCount == 0 && _isSwiping) {
            _isSwiping = false;
            if (!_movingCourutineWorking) StartCorrectionCourutine();
        }
    }

    private void StartCorrectionCourutine() {
        if (SelectedAxis.Equals(Axis.X_AXIS)) {
			if (IsCloserToLeftSide()) {
				StartCoroutine(MoveToXAxisBoundary(true));
			} else {
				StartCoroutine(MoveToXAxisBoundary(false));
			} 
        } else {
            if (IsCloserToBottomSide()) {
                StartCoroutine(MoveToYAxisBoundary(true));
            } else {
                StartCoroutine(MoveToYAxisBoundary(false));
            }
        }
    }

    private bool IsGameObjectHit() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        results.ForEach(result => Debug.Log(result));
        return results.Select(result => result.gameObject.GetComponentInParent<MobileInputSwipeUI>()).Contains(this);
    }



	/**
     *   Methods for X axis swipe only
     */

	private void SwipeObjectByXAxis()
	{
		_transform.position = new Vector2(_transform.position.x + Input.GetTouch(0).deltaPosition.x,
										  _transform.position.y);
		CorrectXPosition();
	}

	private IEnumerator MoveToXAxisBoundary(bool moveToleft)
	{
		float oldY = _transform.anchoredPosition.y;
		_movingCourutineWorking = true;
		if (moveToleft)
		{
			while (_transform.anchoredPosition.x > MinXValue)
			{
				float oldX = _transform.anchoredPosition.x;
				_transform.anchoredPosition = new Vector2(oldX - 10, oldY);
				yield return new WaitForSeconds(0.005f);
			}
		}
		else
		{
			while (_transform.anchoredPosition.x < MaxXValue)
			{
				float oldX = _transform.anchoredPosition.x;
				_transform.anchoredPosition = new Vector2(oldX + 10, oldY);
				yield return new WaitForSeconds(0.005f);
			}
		}
		_movingCourutineWorking = false;
		CorrectXPosition();
	}

	private void CorrectXPosition()
	{
		if (_transform.anchoredPosition.x < MinXValue)
		{
			_transform.anchoredPosition = new Vector2(MinXValue, _transform.anchoredPosition.y);
		}
		else if (_transform.anchoredPosition.x > MaxXValue)
		{
			_transform.anchoredPosition = new Vector2(MaxXValue, _transform.anchoredPosition.y);
		}
	}

	private bool MovingTowardsRightBoundary()
	{
		return _transform.anchoredPosition.x >= MaxXValue && Input.GetTouch(0).deltaPosition.x > 0;
	}

	private bool MovingTowardsLeftBoundary()
	{
		return _transform.anchoredPosition.x <= MinXValue && Input.GetTouch(0).deltaPosition.x < 0;
	}

	private bool IsCloserToLeftSide()
	{
		return _transform.anchoredPosition.x - MinXValue <= MaxXValue - _transform.anchoredPosition.x;
	}

	public bool IsOnLeftSide()
	{
		return Math.Abs(_transform.anchoredPosition.x - MinXValue) < 1;
	}

	public bool IsOnRightSide()
	{
		return Math.Abs(_transform.anchoredPosition.x - MaxXValue) < 1;
	}


	/**
     *   Methods for Y Axis Swipe only
     */

	private void SwipeObjectByYAxis()
	{
		_transform.position = new Vector2(_transform.position.x,
										  _transform.position.y + Input.GetTouch(0).deltaPosition.y);
		CorrectYPosition();

	}

	private IEnumerator MoveToYAxisBoundary(bool moveToBottom)
	{
		float oldX = _transform.anchoredPosition.x;
		_movingCourutineWorking = true;
		if (moveToBottom)
		{
			while (_transform.anchoredPosition.y > MinXValue)
			{
				float oldY = _transform.anchoredPosition.y;
				_transform.anchoredPosition = new Vector2(oldX, oldY - 10);
				yield return new WaitForSeconds(0.005f);
			}
		}
		else
		{
			while (_transform.anchoredPosition.y < MaxXValue)
			{
				float oldY = _transform.anchoredPosition.y;
				_transform.anchoredPosition = new Vector2(oldX, oldY + 10);
				yield return new WaitForSeconds(0.005f);
			}
		}
		_movingCourutineWorking = false;
		CorrectYPosition();
	}

	private void CorrectYPosition()
	{
		if (_transform.anchoredPosition.y < MinXValue)
		{
			_transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x, MinXValue);
		}
		else if (_transform.anchoredPosition.y > MaxXValue)
		{
			_transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x, MaxXValue);
		}
	}

	private bool MovingTowardsTopBoundary()
	{
		return _transform.anchoredPosition.y >= MaxXValue && Input.GetTouch(0).deltaPosition.y > 0;
	}

	private bool MovingTowardsBottomBoundary()
	{
		return _transform.anchoredPosition.y <= MinXValue && Input.GetTouch(0).deltaPosition.y < 0;
	}

	private bool IsCloserToBottomSide()
	{
		return _transform.anchoredPosition.y - MinXValue <= MaxXValue - _transform.anchoredPosition.y;
	}


}