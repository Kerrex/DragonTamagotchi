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

        if (_isSwiping && !MovingTowardsLeftBoundary() && !MovingTowardsRightBoundary()) {
            SwipeObject();
        }
    }

    private void SwipeObject() {
        _transform.anchoredPosition = new Vector2(_transform.anchoredPosition.x + Input.GetTouch(0).deltaPosition.x,
                _transform.anchoredPosition.y);
        CorrectPosition();
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

        if (IsCloserToLeftSide()) {
            StartCoroutine(MoveTo(true));
        } else {
            StartCoroutine(MoveTo(false));
        }
    }

    private IEnumerator MoveTo(bool moveToleft) {
        float oldY = _transform.anchoredPosition.y;
        _movingCourutineWorking = true;
        if (moveToleft) {
            while (_transform.anchoredPosition.x > MinXValue) {
                float oldX = _transform.anchoredPosition.x;
                _transform.anchoredPosition = new Vector2(oldX - 10, oldY);
                yield return new WaitForSeconds(0.005f);
            }
        } else {
            while (_transform.anchoredPosition.x < MaxXValue) {
                float oldX = _transform.anchoredPosition.x;
                _transform.anchoredPosition = new Vector2(oldX + 10, oldY);
                yield return new WaitForSeconds(0.005f);
            }
        }
        _movingCourutineWorking = false;
        CorrectPosition();
    }

    private void CorrectPosition() {
        if (_transform.anchoredPosition.x < MinXValue) {
            _transform.anchoredPosition = new Vector2(MinXValue, _transform.anchoredPosition.y);
        }
        else if (_transform.anchoredPosition.x > MaxXValue) {
            _transform.anchoredPosition = new Vector2(MaxXValue, _transform.anchoredPosition.y);
        }
    }

    private bool MovingTowardsRightBoundary() {
        return _transform.anchoredPosition.x >= MaxXValue && Input.GetTouch(0).deltaPosition.x > 0;
    }

    private bool MovingTowardsLeftBoundary() {
        return _transform.anchoredPosition.x <= MinXValue && Input.GetTouch(0).deltaPosition.x < 0;
    }

    private bool IsGameObjectHit() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        results.ForEach(result => Debug.Log(result));
        return results.Select(result => result.gameObject.GetComponentInParent<MobileInputSwipeUI>()).Contains(this);
    }

    private bool IsCloserToLeftSide() {
        return _transform.anchoredPosition.x - MinXValue <= MaxXValue - _transform.anchoredPosition.x;
    }
}